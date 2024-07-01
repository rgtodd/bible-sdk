using BibleCore.Service.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service
{
    public class TextService(IGlobalGreek globalGreek) : ITextService
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
