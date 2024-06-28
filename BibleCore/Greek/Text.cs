using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Text
    {
        public List<TextEntry> Entries { get; } = [];


        public TextEntry CreateTextEntry(Bookmark bookmark, string text, string word, string normalizedWord, Lexeme lexeme)
        {
            var textEntry = new TextEntry()
            {
                Bookmark = bookmark,
                Text = text,
                Word = word,
                NormalizedWord = normalizedWord,
                Lexeme = lexeme
            };

            Entries.Add(textEntry);

            return textEntry;
        }

        public IEnumerable<TextEntry> Select(Range range)
        {
            Console.WriteLine($"range = {range}");

            foreach (var entry in Entries)
            {
                if (entry.Bookmark.Book == Book.FirstJohn && entry.Bookmark.Chapter == 1)
                {
                    yield return entry;
                }
            }

        }
    }
}
