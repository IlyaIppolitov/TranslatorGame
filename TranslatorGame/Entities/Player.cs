using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorGame.Entities
{
    public class Player
    {
        public Guid Id { get; init; }
        public string Login { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Word>? Words { get; set; } = new List<Word>();
        public override string ToString()
        {
            return ($"{Login}");
        }
    }
}
