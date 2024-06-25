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


        public TextEntry CreateTextEntry(TextEntryBookmark bookmark, string text, string word, string normalizedWord)
        {
            var textEntry = new TextEntry()
            {
                Bookmark = bookmark,
                Text = text,
                Word = word,
                NormalizedWord = normalizedWord
            };

            Entries.Add(textEntry);

            return textEntry;
        }
    }
}
