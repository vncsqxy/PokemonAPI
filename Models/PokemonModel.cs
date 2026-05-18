using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // 🔥 IMPORTANTE: Necessário para os atributos de tradução

namespace PokeData.IngestionApp.Models
{
    public class PokemonModel
    {
        // O [JsonIgnore] diz para o RestSharp NÃO enviar esse campo para o Erick, já que o Firestore dele gera o ID lá.
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string Nome { get; set; }

        // O [JsonPropertyName] garante que o nome vá em minúsculo e em português exatamente como o Swagger dele pede
        [JsonPropertyName("altura")]
        public double Altura { get; set; }

        [JsonPropertyName("peso")]
        public double Peso { get; set; }

        [JsonPropertyName("tipos")]
        public List<string> Tipos { get; set; } = new List<string>();

        [JsonPropertyName("spriteUrl")]
        public string SpriteUrl { get; set; }

        [JsonPropertyName("hp")] // Corrigido de HP para hp
        public int HP { get; set; }

        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("defense")]
        public int Defense { get; set; }

        [JsonPropertyName("spAttack")] // Mapeia perfeitamente o camelCase do Swagger dele
        public int SpAttack { get; set; }

        [JsonPropertyName("spDefense")]
        public int SpDefense { get; set; }

        [JsonPropertyName("speed")]
        public int Speed { get; set; }

        [JsonPropertyName("baseStatTotal")]
        public int BaseStatTotal { get; set; }

        [JsonPropertyName("competitiveRole")]
        public string CompetitiveRole { get; set; }

        [JsonIgnore]
        public DateTime DataColeta { get; set; } = DateTime.Now;

        [JsonIgnore]
        public bool EnviadoParaNuvem { get; set; } = false;
    }
}