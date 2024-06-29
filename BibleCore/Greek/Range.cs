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

        public static Range? Parse(string text)
        {
            int idxDash = text.IndexOf('-');
            if (idxDash != -1)
            {
                var textFrom = text[..idxDash];
                var textTo = text[(idxDash + 1)..];

                var bookmarkFrom = Bookmark.Parse(textFrom);
                var bookmarkTo = Bookmark.Parse(textTo);

                return bookmarkFrom != null && bookmarkTo != null
                    ? new Range()
                    {
                        From = bookmarkFrom.Value,
                        To = bookmarkTo.Value
                    }
                    : null;
            }
            else
            {
                var bookmark = Bookmark.Parse(text);

                return bookmark != null
                    ? new Range()
                    {
                        From = bookmark.Value,
                        To = bookmark.Value
                    }
                    : null;
            }
        }

        public string Format()
        {
            var sb = new StringBuilder();

            sb.Append(From.Format());
            if (To != From)
            {
                sb.Append(" - ");
                sb.Append(To.Format());
            }

            return sb.ToString();
        }

        public bool Contains(Bookmark bookmark)
        {
            return bookmark >= From && bookmark <= To;
        }
    }
}
