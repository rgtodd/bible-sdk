using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class TextVerseData
    {
        public required BookData Book { get; init;}

        public required byte Chapter { get; init;  }

        public required byte Verse { get; init; }

        public required TextWordData[] Words { get; init; }
    }
}
