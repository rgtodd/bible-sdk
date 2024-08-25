namespace BibleCore.Greek
{
    internal class Form
    {
        public required Lexeme Lexeme { get; init; }

        public required string InflectedForm { get; init; }

        public required string InflectedTransliteration { get; init; }

        public string Prefix { get; set; } = "";

        public string Core { get; set; } = "";

        public string Suffix { get; set; } = "";

        public Inflection Inflection { get; init; }

        public List<Bookmark> Bookmarks { get; } = [];

    }
}
