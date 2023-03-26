using System;
using System.Collections.Generic;

namespace TranslatorGame.Entities
{
    public class Word
    {
        public Guid Id { get; init; }
        public string? RussianText { get; set; }
        public string? EnglishText { get; set; }
        public string? GermanText { get; set; }
        public Dictionary Dictionary { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public override string ToString()
        {
            return ($"{RussianText}");
        }
    }
}
