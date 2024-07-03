namespace BibleCore.Service.Data
{
    public class TextVerseData
    {
        public required BookmarkData Bookmark { get; init; }

        public required TextWordData[] Words { get; init; }
    }
}
