﻿using System.Text;
using System.Text.RegularExpressions;

namespace BibleCore.Greek
{
    internal readonly struct Bookmark : IEquatable<Bookmark>, IComparable<Bookmark>
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
            { "phil", Book.Philippians },
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

            { "2 corinthians", Book.SecondCorinthians },
            { "2 john", Book.SecondJohn },
            { "2 peter", Book.SecondPeter },
            { "2 thessalonians", Book.SecondThessalonians },
            { "2 timothy", Book.SecondTimothy },

            { "3 john", Book.ThirdJohn }
        };

        private readonly static string s_referenceRegexExpression = @"^(\d?)\s*(\w+)\s*(?:(\d+)\s*(?::\s*(\d+))?)?$";

        private readonly static Regex s_referenceRegex = new(s_referenceRegexExpression);

        public required readonly Book Book { get; init; }

        public required readonly byte Chapter { get; init; }

        public required readonly byte Verse { get; init; }

        public static Bookmark Create(Book book, byte chapter, byte verse)
        {
            return new Bookmark()
            {
                Book = book,
                Chapter = chapter,
                Verse = verse
            };
        }

        public static Bookmark Parse(string text)
        {
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            text = text.Trim().ToLower();

            var match = s_referenceRegex.Match(text);
            if (!match.Success)
            {
                throw new ArgumentException("Text is not a valid range expression.");
            }

            var number = match.Groups[1].Value;
            var bookName = match.Groups[2].Value;
            var chapter = match.Groups[3].Value;
            var verse = match.Groups[4].Value;

            string title = !string.IsNullOrEmpty(number) ? $"{number} {bookName}" : bookName;
            var book = ParseBook(title) ?? throw new ArgumentException($"Unknown book title {title}.");
            var chapterValue = string.IsNullOrEmpty(chapter) ? (byte)0 : byte.Parse(chapter);

            var verseValue = string.IsNullOrEmpty(verse) ? (byte)0 : byte.Parse(verse);

            return Create(book, chapterValue, verseValue);
        }

        public static string FormatBook(Book book)
        {
            return s_shortTitlesByBook.TryGetValue(book, out var name) ? name : throw new NotImplementedException();
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

        public string Format()
        {
            var sb = new StringBuilder();

            sb.Append(FormatBook(Book));
            if (Chapter != 0)
            {
                sb.Append(' ');
                sb.Append(Chapter);
                if (Verse != 0)
                {
                    sb.Append(':');
                    sb.Append(Verse);
                }
            }

            return sb.ToString();
        }

        public Bookmark ToUpperBound()
        {
            return Create(Book, Chapter == 0 ? byte.MaxValue : Chapter, Verse == 0 ? byte.MaxValue : Verse);
        }

        public override string ToString()
        {
            return $"{Book} {Chapter}:{Verse}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Bookmark bookmark && Equals(bookmark);
        }

        public bool Equals(Bookmark other)
        {
            return Book == other.Book &&
                   Chapter == other.Chapter &&
                   Verse == other.Verse;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Book, Chapter, Verse);
        }

        public int CompareTo(Bookmark other)
        {
            var result = Book.CompareTo(other.Book);
            if (result == 0)
            {
                result = Chapter.CompareTo(other.Chapter);
            }
            if (result == 0)
            {
                result = Verse.CompareTo(other.Verse);
            }
            return result;
        }

        public static bool operator ==(Bookmark left, Bookmark right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Bookmark left, Bookmark right)
        {
            return !(left == right);
        }

        public static bool operator <(Bookmark left, Bookmark right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Bookmark left, Bookmark right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Bookmark left, Bookmark right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Bookmark left, Bookmark right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
