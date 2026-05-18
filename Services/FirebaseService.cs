using System;
using System.Threading.Tasks;
using RestSharp;
using PokeData.IngestionApp.Models;

namespace PokeData.IngestionApp.Services
{
    public class FirebaseService
    {
        private readonly RestClient _client;

        public FirebaseService()
        {
            
            _client = new RestClient("https://bancopokeapi-default-rtdb.firebaseio.com/");
        }

        public async Task SalvarPokemonAsync(PokemonModel pokemon)
        {
            // O Firebase exige que coloquemos ".json" no final do nó onde queremos salvar os dados
            // O Method.Post cria um registro novo com um ID único automático (Hash) no Firebase
            var request = new RestRequest("pokemons_coletados.json", Method.Post);

            // Atualiza a flag de controle do nosso modelo
            pokemon.EnviadoParaNuvem = true;

            // Transforma o nosso Pokémon em JSON e coloca no corpo da requisição
            request.AddJsonBody(pokemon);

            // Envia para o Firebase
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Falha ao salvar no Firebase. Verifique sua conexão e as regras do banco.");
            }
        }
    }
}