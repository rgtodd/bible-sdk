using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Form
    {
        public required Lexeme Lexeme { get; init; }

        public required string Word { get; init; }

        public string Prefix { get; set; } = "";

        public string Core { get; set; } = "";

        public string Suffix { get; set; } = "";

        public Inflection Inflection { get; init; }

        public List<Bookmark> Bookmarks { get; } = [];

    }
}
