using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class TextVerseData
    {
        public required string Reference { get; init; }

        public required TextWordData[] Words { get; init; }
    }
}
