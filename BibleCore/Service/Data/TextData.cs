namespace BibleCore.Service.Data
{
    public class TextData
    {
        public required string RangeExpression { get; init; }

        public required TextVerseData[] Verses { get; init; }
    }
}
