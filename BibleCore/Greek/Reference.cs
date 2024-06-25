using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public readonly struct Reference
    {
        public required TextEntryBookmark Bookmark { get; init; }
    }
}
