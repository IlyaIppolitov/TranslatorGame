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
        public string RussianName { get; set;}
        public string EnglishName { get; set; }
        public string GermanName { get; set; }
        public List<Dictionary> Dictionaries { get; set; } = new List<Dictionary>();
        public override string ToString()
        {
            return ($"{RussianName}");
        }
    }
}
