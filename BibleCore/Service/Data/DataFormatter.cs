using BibleCore.Greek;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Core.Tokens;

namespace BibleCore.Service.Data
{
    public static class DataFormatter
    {
        public static string FormatBook(Book book)
        {
            return book switch
            {
                Book.Matthew => "Mt",
                Book.Mark => "Mk",
                Book.Luke => "Lk",
                Book.John => "Jn",
                Book.Acts => "Acts",
                Book.Romans => "Rom",
                Book.FirstCorinthians => "1 Cor",
                Book.SecondCorinthians => "2 Cor",
                Book.Galatians => "Gal",
                Book.Ephesians => "Eph",
                Book.Philippians => "Phil",
                Book.Colossians => "Col",
                Book.FirstThessalonians => "1 Thes",
                Book.SecondThessalonians => "2 Thes",
                Book.FirstTimothy => "1 Tm",
                Book.SecondTimothy => "2 Tm",
                Book.Titus => "Ti",
                Book.Philemon => "Phil",
                Book.Hebrews => "Heb",
                Book.James => "Jas",
                Book.FirstPeter => "1 Pt",
                Book.SecondPeter => "2 Pt",
                Book.FirstJohn => "1 Jn",
                Book.SecondJohn => "2 Jn",
                Book.ThirdJohn => "3 Jn",
                Book.Jude => "Jude",
                Book.Revelation => "Rv",
                _ => throw new NotImplementedException()
            };
        }
    }
}
