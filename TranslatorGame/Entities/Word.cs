﻿using System;
using System.Collections.Generic;

namespace TranslatorGame.Entities
{
    public class Word
    {
        public Guid Id { get; set; }
        public string? Russian { get; set; }
        public string? English { get; set; }
        public string? German { get; set; }
        public Category? Category { get; set; }
        public List<Player>? Players { get; set; } = new List<Player>();
        public uint? Mistakes { get; set; }
        public override string ToString()
        {
            return ($"{Russian}");
        }
    }
}
