using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorGame.Entities
{
    public class Category
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<Dictionary> Dictionaries { get; set; } = new List<Dictionary>();
        public override string ToString()
        {
            return ($"{Name}");
        }
    }
}
