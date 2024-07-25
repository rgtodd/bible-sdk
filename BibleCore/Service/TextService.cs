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

            var textData = Select(range);

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

            var textData = Select(range);

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

            var textData = Select(range);

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

            var textData = Select(range);

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

            var textData = Select(range);

            return textData;
        }

        private TextData? Select(Greek.Range range)
        {
            var textEntries = globalGreek.Text.Select(range, 5000);
            var apparatusEntries = globalGreek.Apparatus.Select(range, 100);

            var apparatusDictionary = apparatusEntries.GroupBy(a => a.Bookmark).ToDictionary(g => g.Key, g => g.ToList());

            var textData = DataFactory.CreateTextData(range, textEntries, apparatusDictionary);

            return textData;
        }
    }
}
