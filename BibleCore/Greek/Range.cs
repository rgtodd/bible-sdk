using System.Text;

namespace BibleCore.Greek
{
    internal class Range
    {
        public required Bookmark From { get; init; }

        public required Bookmark To { get; init; }

        public static Range Parse(string text)
        {
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            int idxDash = text.IndexOf('-');
            if (idxDash != -1)
            {
                var textFrom = text[..idxDash];
                var textTo = text[(idxDash + 1)..];

                var bookmarkFrom = Bookmark.Parse(textFrom);
                var bookmarkTo = Bookmark.Parse(textTo);

                return new Range()
                {
                    From = bookmarkFrom,
                    To = bookmarkTo
                };
            }
            else
            {
                var bookmark = Bookmark.Parse(text);

                return new Range()
                {
                    From = bookmark,
                    To = bookmark
                };
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
