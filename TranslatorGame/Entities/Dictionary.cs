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
        public Category Category { get; set; }
        public List<Word>? Words { get; set; }
        public override string ToString()
        {
            return ($"{Category.Name}");
        }
    }
}
