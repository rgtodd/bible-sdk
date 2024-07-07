namespace BibleCore.Greek
{
    internal class Text
    {
        public List<TextEntry> Entries { get; } = [];

        public Dictionary<Book, Dictionary<byte, Dictionary<byte, int>>> Counts { get; } = [];

        public TextEntry CreateTextEntry(Bookmark bookmark, byte position, string text, string word, string normalizedWord, Lexeme lexeme)
        {
            var textEntry = new TextEntry()
            {
                Bookmark = bookmark,
                Position = position,
                Text = text,
                Word = word,
                NormalizedWord = normalizedWord,
                Lexeme = lexeme
            };

            Entries.Add(textEntry);

            var bookEntry = Counts.GetValueOrDefault(bookmark.Book);
            if (bookEntry == null)
            {
                bookEntry = [];
                Counts.Add(bookmark.Book, bookEntry);
            }

            var chapterEntry = bookEntry.GetValueOrDefault(bookmark.Chapter);
            if (chapterEntry == null)
            {
                chapterEntry = [];
                bookEntry.Add(bookmark.Chapter, chapterEntry);
            }

            chapterEntry[bookmark.Verse] =
                chapterEntry.TryGetValue(bookmark.Verse, out var verseCount)
                ? verseCount + 1
                : 1;

            return textEntry;
        }

        public IEnumerable<TextEntry> Select(Range range, int maxEntries)
        {
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
