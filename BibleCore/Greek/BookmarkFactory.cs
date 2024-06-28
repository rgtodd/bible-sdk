using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public static class BookmarkFactory
    {
        private readonly static Dictionary<Book, string> s_shortTitlesByBook = new()
        {
            { Book.Acts, "Acts" },
            { Book.Colossians, "Col" },
            { Book.Ephesians, "Eph" },
            { Book.Galatians, "Gal" },
            { Book.Hebrews, "Heb" },
            { Book.James, "Jas" },
            { Book.John, "John" },
            { Book.Jude, "Jude" },
            { Book.Luke, "Luke" },
            { Book.Matthew, "Matt" },
            { Book.Mark, "Mark" },
            { Book.Philemon, "Phlm" },
            { Book.Philippians, "Phil" },
            { Book.Revelation, "Rev" },
            { Book.Romans, "Rom" },
            { Book.Titus, "Titus" },

            { Book.FirstCorinthians, "1 Cor" },
            { Book.FirstJohn, "1 John" },
            { Book.FirstPeter, "1 Pet" },
            { Book.FirstThessalonians, "1 Thess" },
            { Book.FirstTimothy, "1 Tim" },

            { Book.SecondCorinthians, "2 Cor" },
            { Book.SecondJohn, "2 John" },
            { Book.SecondPeter, "2 Pet" },
            { Book.SecondThessalonians, "2 Thess" },
            { Book.SecondTimothy, "2 Tim" },

            { Book.ThirdJohn, "3 John" }
        };

        private readonly static Dictionary<string, Book> s_booksByTitle = new()
        {
            { "acts", Book.Acts },
            { "colossians", Book.Colossians },
            { "ephesians", Book.Ephesians },
            { "galatians", Book.Galatians },
            { "hebrews", Book.Hebrews},
            { "james", Book.James  },
            { "jas", Book.James  },
            { "john", Book.John  },
            { "jude", Book.Jude },
            { "luke", Book.Luke },
            { "matthew", Book.Matthew },
            { "mark", Book.Mark },
            { "phil", Book.Philemon },
            { "philemon", Book.Philemon },
            { "philippians", Book.Philippians },
            { "phlm", Book.Philemon },
            { "revelation", Book.Revelation },
            { "romans", Book.Romans },
            { "titus", Book.Titus },

            { "1 corinthians", Book.FirstCorinthians },
            { "1 john", Book.FirstJohn },
            { "1 peter", Book.FirstPeter },
            { "1 thessalonians", Book.FirstThessalonians },
            { "1 timothy", Book.FirstTimothy },

            { "2 corinthians ", Book.SecondCorinthians },
            { "2 john", Book.SecondJohn },
            { "2 peter", Book.SecondPeter },
            { "2 thessalonians", Book.SecondThessalonians },
            { "2 timothy", Book.SecondTimothy },

            { "3 john", Book.ThirdJohn }
        };

        private readonly static string s_referenceRegexExpression = @"^(\d?)\s*(\w+)\s*(?:(\d+)\s*(?::\s*(\d+))?)?$";

        private readonly static Regex s_referenceRegex = new(s_referenceRegexExpression);

        public static string GetShortTitle(Book book)
        {
            return s_shortTitlesByBook.TryGetValue(book, out var name) ? name : throw new NotImplementedException();
        }

        public static string Format(Bookmark bookmark)
        {
            var sb = new StringBuilder();

            sb.Append(GetShortTitle(bookmark.Book));
            if (bookmark.Chapter != 0)
            {
                sb.Append(' ');
                sb.Append(bookmark.Chapter);
                if (bookmark.Verse != 0)
                {
                    sb.Append(':');
                    sb.Append(bookmark.Verse);
                }
            }

            return sb.ToString();
        }

        public static string Format(Range range)
        {
            var sb = new StringBuilder();

            sb.Append(Format(range.From));
            if (range.To != range.From)
            {
                sb.Append(" - ");
                sb.Append(Format(range.To));
            }

            return sb.ToString();
        }

        public static Bookmark? ParseBookmark(string text)
        {
            text = text.Trim().ToLower();

            var match = s_referenceRegex.Match(text);
            if (!match.Success)
            {
                return null;
            }

            var number = match.Groups[1].Value;
            var bookName = match.Groups[2].Value;
            var chapter = match.Groups[3].Value;
            var verse = match.Groups[4].Value;

            string title = !string.IsNullOrEmpty(number) ? $"{number} {bookName}" : bookName;
            var book = ParseBook(title);
            if (book == null)
            {
                return null;
            }

            var chapterValue = string.IsNullOrEmpty(chapter) ? (byte)0 : byte.Parse(chapter);

            var verseValue = string.IsNullOrEmpty(verse) ? (byte)0 : byte.Parse(verse);

            return new Bookmark()
            {
                Book = book.Value,
                Chapter = chapterValue,
                Verse = verseValue,
                Position = 0
            };
        }

        public static Range? ParseRange(string text)
        {
            int idxDash = text.IndexOf('-');
            if (idxDash != -1)
            {
                var textFrom = text.Substring(0, idxDash);
                var textTo = text.Substring(idxDash + 1);

                var bookmarkFrom = ParseBookmark(textFrom);
                var bookmarkTo = ParseBookmark(textTo);

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
                var bookmark = ParseBookmark(text);

                return bookmark != null
                    ? new Range()
                    {
                        From = bookmark.Value,
                        To = bookmark.Value
                    }
                    : null;
            }
        }

        public static Book? ParseBook(string text)
        {
            string? currentKnownTitle = null;

            foreach (var knownTitle in s_booksByTitle.Keys)
            {
                if (knownTitle.StartsWith(text))
                {
                    if (currentKnownTitle == null || knownTitle.Length < currentKnownTitle.Length)
                    {
                        currentKnownTitle = knownTitle;
                    }
                }
            }

            return currentKnownTitle == null ? null : s_booksByTitle[currentKnownTitle];
        }
    }

}
