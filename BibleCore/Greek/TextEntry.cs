using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class TextEntry
    {
        public required Bookmark Bookmark { get; init; }

        public required string Text { get; init; }

        public required string Word { get; init; }

        public required string NormalizedWord { get; init; }

        public required Lexeme Lexeme { get; init; }

    }
}
