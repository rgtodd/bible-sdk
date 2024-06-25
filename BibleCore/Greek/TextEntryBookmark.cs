using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public readonly struct TextEntryBookmark : IEquatable<TextEntryBookmark>, IComparable<TextEntryBookmark>
    {
        public readonly Books Book { get; init; }

        public readonly byte Chapter { get; init; }

        public readonly byte Verse { get; init; }

        public readonly byte Position { get; init; }

        public override bool Equals(object? obj)
        {
            return obj is TextEntryBookmark bookmark && Equals(bookmark);
        }

        public bool Equals(TextEntryBookmark other)
        {
            return Book == other.Book &&
                   Chapter == other.Chapter &&
                   Verse == other.Verse &&
                   Position == other.Position;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Book, Chapter, Verse, Position);
        }

        public int CompareTo(TextEntryBookmark other)
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
            if (result == 0)
            {
                result = Position.CompareTo(other.Position);
            }
            return result;
        }

        public static bool operator ==(TextEntryBookmark left, TextEntryBookmark right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TextEntryBookmark left, TextEntryBookmark right)
        {
            return !(left == right);
        }

        public static bool operator <(TextEntryBookmark left, TextEntryBookmark right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(TextEntryBookmark left, TextEntryBookmark right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(TextEntryBookmark left, TextEntryBookmark right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(TextEntryBookmark left, TextEntryBookmark right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
