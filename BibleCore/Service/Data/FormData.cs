namespace BibleCore.Service.Data
{
    public class FormData
    {
        public required string InflectedForm { get; init; }

        public required string InflectedTransliteration { get; init; }

        public required string Prefix { get; init; }

        public required string Core { get; init; }

        public required string Suffix { get; init; }

        public required InflectionData Inflection { get; init; }

        public required BookmarkData[] Bookmarks { get; init; }

    }
}
