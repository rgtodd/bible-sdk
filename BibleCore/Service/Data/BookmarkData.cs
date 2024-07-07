namespace BibleCore.Service.Data
{
    public class BookmarkData
    {
        public required BookData Book { get; init; }

        public required byte Chapter { get; init; }

        public required byte Verse { get; init; }


        public required string FormattedBookmark { get; init; }

        public required string FormattedBook { get; init; }
    }
}
