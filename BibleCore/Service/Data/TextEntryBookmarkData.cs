using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class TextEntryBookmarkData
    {
        public required BookData Book { get; init; }

        public required byte Chapter { get; init; }

        public required byte Verse { get; init; }

        public required byte Position { get; init; }

        public required string FormattedBookmark { get; init; }
    }
}
