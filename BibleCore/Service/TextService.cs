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

        public TextData? MoveNextText(string? rangeExpression)
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

            range = globalGreek.Text.MoveNext(range);
            if (range == null)
            {
                return null;
            }

            var textEntries = globalGreek.Text.Select(range, 5000);

            var textData = DataFactory.CreateTextData(range, textEntries);

            return textData;
        }

        public TextData? MovePreviousText(string? rangeExpression)
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

            range = globalGreek.Text.MovePrevious(range);
            if (range == null)
            {
                return null;
            }

            var textEntries = globalGreek.Text.Select(range, 5000);

            var textData = DataFactory.CreateTextData(range, textEntries);

            return textData;
        }

        public TextData? ExtendNextText(string? rangeExpression)
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

            range = globalGreek.Text.ExtendNext(range);
            if (range == null)
            {
                return null;
            }

            var textEntries = globalGreek.Text.Select(range, 5000);

            var textData = DataFactory.CreateTextData(range, textEntries);

            return textData;
        }

        public TextData? ExtendPreviousText(string? rangeExpression)
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

            range = globalGreek.Text.ExtendPrevious(range);
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
