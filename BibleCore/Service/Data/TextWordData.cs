namespace BibleCore.Service.Data
{
    public class TextWordData
    {
        public required byte Position { get; init; }

        public required string Word { get; init; }

        public required string Transliteration { get; init; }

        public required int? StrongsNumber { get; init; }

        public required int? GkNumber { get; init; }
    }
}
