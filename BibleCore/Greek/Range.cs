using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Range
    {
        public required Bookmark From { get; init; }
        
        public required Bookmark To { get; init; }

        public bool Contains(Bookmark bookmark)
        {
            return bookmark >= From && bookmark <= To;
        }
    }
}
