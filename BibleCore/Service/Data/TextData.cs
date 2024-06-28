using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class TextData
    {
        public required string RangeExpression { get; init; }

        public required TextVerseData[] Verses { get; init; }
    }
}
