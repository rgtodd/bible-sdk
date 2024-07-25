namespace BibleCore.Greek
{
    internal class Apparatus
    {
        private List<ApparatusEntry> Entries { get; } = [];

        public ApparatusEntry CreateApparatusEntry(Bookmark bookmark, string note)
        {
            var apparatusEntry = new ApparatusEntry()
            {
                Bookmark = bookmark,
                Note = note
            };
            Entries.Add(apparatusEntry);

            return apparatusEntry;
        }

        public IEnumerable<ApparatusEntry> Select(Range range, int maxEntries)
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
