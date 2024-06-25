using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public readonly struct TextEntryBookmark
    {
        public readonly Books Book { get; init; }

        public readonly byte Chapter { get; init; }

        public readonly byte Verse { get; init; }

        public readonly byte Position { get; init; }

    }
}
