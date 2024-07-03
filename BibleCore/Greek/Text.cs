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

        public IEnumerable<TextEntry> Select(Range range, int maxEntries)
        {
            Console.WriteLine($"range = {range}");

            var adjustedRange = new Range()
            {
                From = range.From,
                To = range.To.ToUpperBound()
            };

            int count = 0;
            foreach (var entry in Entries)
            {
                if (adjustedRange.Contains(entry.Bookmark))
                {
                    yield return entry;
                    if (++count == maxEntries)
                    {
                        yield break;
                    }
                }
            }

        }
    }
}
