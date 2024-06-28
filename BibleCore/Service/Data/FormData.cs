using BibleCore.Greek;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class FormData
    {
        public required string Word { get; init; }

        public required InflectionData Inflection { get; init; }

        public required BookmarkData[] Bookmarks { get; init; }

    }
}
