using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PokeData.IngestionApp.Models;
using PokeData.IngestionApp.Services;

namespace PokeData.IngestionApp
{
    public partial class MainWindow : Window
    {
        private readonly PokeApiService _pokeApiService;
        private readonly FirebaseService _firebaseService; // Adicionamos o Firebase aqui
        private PokemonModel _pokemonAtual;

        public MainWindow()
        {
            InitializeComponent();
            _pokeApiService = new PokeApiService();
            _firebaseService = new FirebaseService(); // Iniciamos o Firebase aqui
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusca.Text))
            {
                MessageBox.Show("Digite o nome ou ID de um Pokémon!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                txtStatus.Text = "Buscando dados na PokeAPI...";
                btnBuscar.IsEnabled = false;
                cardResultado.Visibility = Visibility.Collapsed;

                // 1. Pega o que o usuário digitou
                string termoBusca = txtBusca.Text.Trim().ToLower();

                // 2. Pega o que está selecionado no Menu Suspenso
                string forma = (cmbForma.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString() ?? "Padrão";

                // 3. Adiciona o sufixo correto magicamente
                if (forma == "Mega") termoBusca += "-mega";
                else if (forma == "Mega X") termoBusca += "-mega-x";
                else if (forma == "Mega Y") termoBusca += "-mega-y";
                else if (forma == "G-Max") termoBusca += "-gmax";

                _pokemonAtual = await _pokeApiService.BuscarPokemonAsync(termoBusca);

                // Preenchendo os dados na tela
                txtNome.Text = _pokemonAtual.Nome.ToUpper();
                txtId.Text = _pokemonAtual.Id.ToString();
                txtTipos.Text = string.Join(", ", _pokemonAtual.Tipos);
                txtBst.Text = _pokemonAtual.BaseStatTotal.ToString();
                txtClasse.Text = _pokemonAtual.CompetitiveRole;

                // Carregando a imagem
                if (!string.IsNullOrEmpty(_pokemonAtual.SpriteUrl))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_pokemonAtual.SpriteUrl, UriKind.Absolute);
                    bitmap.EndInit();
                    imgPokemon.Source = bitmap;
                }

                cardResultado.Visibility = Visibility.Visible;
                txtStatus.Text = "Busca concluída com sucesso!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro na Busca", MessageBoxButton.OK, MessageBoxImage.Error);
                txtStatus.Text = "Erro ao buscar Pokémon.";
            }
            finally
            {
                btnBuscar.IsEnabled = true;
            }
        }

        // --- MUDANÇA AQUI: Botão de salvar agora é assíncrono (async) e chama o Firebase ---
        private async void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_pokemonAtual == null)
            {
                MessageBox.Show("Busque um Pokémon primeiro antes de tentar salvar!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                btnSalvar.IsEnabled = false;
                txtStatus.Text = "Enviando dados para o Firebase...";

                // Manda o pokemon atual para a nuvem!
                await _firebaseService.SalvarPokemonAsync(_pokemonAtual);

                MessageBox.Show($"{_pokemonAtual.Nome.ToUpper()} salvo na nuvem com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                txtStatus.Text = "Sincronizado com a nuvem!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no Firebase: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                txtStatus.Text = "Falha na sincronização.";
            }
            finally
            {
                btnSalvar.IsEnabled = true;
            }
        }
    }
}