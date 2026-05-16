using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using PokeData.IngestionApp.Models;

namespace PokeData.IngestionApp.Services
{
    public class PokeApiService
    {
        private readonly RestClient _client;

        public PokeApiService()
        {
            _client = new RestClient("https://pokeapi.co/api/v2/");
        }

        public async Task<PokemonModel> BuscarPokemonAsync(string nomeOuId)
        {
            var request = new RestRequest($"pokemon/{nomeOuId.ToLower().Trim()}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Pokémon não encontrado! Verifique se o nome ou ID está correto.");

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiData = JsonSerializer.Deserialize<PokeApiDto>(response.Content, jsonOptions);

            int hp = PegarStatus(apiData, "hp");
            int atk = PegarStatus(apiData, "attack");
            int def = PegarStatus(apiData, "defense");
            int spAtk = PegarStatus(apiData, "special-attack");
            int spDef = PegarStatus(apiData, "special-defense");
            int speed = PegarStatus(apiData, "speed");

            return new PokemonModel
            {
                Id = apiData.Id,
                Nome = apiData.Name,
                Altura = apiData.Height / 10.0,
                Peso = apiData.Weight / 10.0,
                Tipos = apiData.Types.Select(t => t.Type.Name).ToList(),
                SpriteUrl = apiData.Sprites?.Front_Default,

                HP = hp,
                Attack = atk,
                Defense = def,
                SpAttack = spAtk,
                SpDefense = spDef,
                Speed = speed,
                BaseStatTotal = hp + atk + def + spAtk + spDef + speed,
                CompetitiveRole = DefinirClasse(speed, atk, spAtk, hp, def, spDef)
            };
        }

        private int PegarStatus(PokeApiDto dados, string nomeStatus)
        {
            var status = dados.Stats.FirstOrDefault(s => s.Stat.Name == nomeStatus);
            return status != null ? status.Base_Stat : 0;
        }

        private string DefinirClasse(int speed, int atk, int spAtk, int hp, int def, int spDef)
        {
            if (speed >= 100 && (atk >= 100 || spAtk >= 100)) return "Fast Sweeper (Atacante Rápido)";
            if (hp >= 100 && def >= 100) return "Physical Wall (Barreira Física)";
            if (hp >= 100 && spDef >= 100) return "Special Wall (Barreira Especial)";
            if (atk > spAtk) return "Physical Attacker (Atacante Físico)";
            if (spAtk > atk) return "Special Attacker (Atacante Especial)";
            return "Balanced (Balanceado)";
        }

        // DTOs
        private class PokeApiDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public PokeType[] Types { get; set; }
            public PokeStat[] Stats { get; set; }
            public PokeSprites Sprites { get; set; }
        }
        private class PokeType { public PokeTypeName Type { get; set; } }
        private class PokeTypeName { public string Name { get; set; } }
        private class PokeStat { public int Base_Stat { get; set; } public PokeStatName Stat { get; set; } }
        private class PokeStatName { public string Name { get; set; } }
        private class PokeSprites { public string Front_Default { get; set; } }
    }
}