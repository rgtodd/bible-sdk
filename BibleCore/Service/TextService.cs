using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class TextService(IGlobalGreek globalGreek) : ITextService
    {
        public TextData? GetText(string? rangeExpression)
        {
            if (rangeExpression == null)
            {
                return null;
            }

            var range = Greek.Range.Parse(rangeExpression);
            if (range == null)
            {
                return null;
            }

            var textEntries = globalGreek.Text.Select(range, 5000);

            var textData = DataFactory.CreateTextData(range, textEntries);

            return textData;
        }
    }
}
