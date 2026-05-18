using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.Json; // Importante para a configuração do JSON
using System.Text.Json;
using PokeData.IngestionApp.Models;

namespace PokeData.IngestionApp.Services
{
    public class ErickApiService
    {
        private readonly RestClient _client;

        // 🔥 LINK OFICIAL: Conectado direto na Google Cloud do Erick!
        private const string BaseUrlGoogleCloud = "https://pokeapi-backend-366354054678.southamerica-east1.run.app";

        public ErickApiService()
        {
            // Configura o cliente RestSharp para usar camelCase por padrão (evita incompatibilidade de maiúsculas/minúsculas)
            var options = new RestClientOptions(BaseUrlGoogleCloud);
            _client = new RestClient(options, configureSerialization: s =>
                s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        /// <summary>
        /// Envia o Pokémon tratado para a API REST do Erick hospedada no Google Cloud Run
        /// </summary>
        public async Task EnviarParaCoreApiAsync(PokemonModel pokemon)
        {
            
            string rotaEndpoint = "api/PokemonData/coleta";

            var request = new RestRequest(rotaEndpoint, Method.Post);

            
            request.AddJsonBody(pokemon);

            var response = await _client.ExecuteAsync(request);

                
            if (!response.IsSuccessful)
            {
                // 🚨 DIAGNÓSTICO SÊNIOR: Printa o status exato na Janela de Saída (Output)
                System.Diagnostics.Debug.WriteLine("=== [DIAGNÓSTICO GOOGLE CLOUD] ===");
                System.Diagnostics.Debug.WriteLine($"URL chamada: {BaseUrlGoogleCloud}/{rotaEndpoint}");
                System.Diagnostics.Debug.WriteLine($"Status HTTP Retornado: {(int)response.StatusCode} ({response.StatusCode})");
                System.Diagnostics.Debug.WriteLine($"Erro do RestSharp: {response.ErrorMessage}");
                System.Diagnostics.Debug.WriteLine($"Conteúdo bruto retornado pelo servidor: {response.Content}");
                System.Diagnostics.Debug.WriteLine("=================================");

                throw new Exception($"Erro na Cloud do Erick (Status {(int)response.StatusCode}): {response.StatusCode}. Verifique a Janela de Saída.");
            }
        }
    }
}