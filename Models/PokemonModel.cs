using System;
using System.Collections.Generic;

namespace PokeData.IngestionApp.Models
{
    public class PokemonModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public List<string> Tipos { get; set; } = new List<string>();
        public string SpriteUrl { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public int BaseStatTotal { get; set; }
        public string CompetitiveRole { get; set; }
        public DateTime DataColeta { get; set; } = DateTime.Now;
        public bool EnviadoParaNuvem { get; set; } = false;
    }
}