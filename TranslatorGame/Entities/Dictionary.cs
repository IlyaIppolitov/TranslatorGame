using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorGame.Entities
{
    public class Dictionary
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Word>? Words { get; set; } = new List<Word>();
        public override string ToString()
        {
            return ($"{Name}");
        }
    }
}
