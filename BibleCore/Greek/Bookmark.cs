using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public readonly struct Bookmark : IEquatable<Bookmark>, IComparable<Bookmark>
    {
        public readonly Book Book { get; init; }

        public readonly byte Chapter { get; init; }

        public readonly byte Verse { get; init; }

        public readonly byte Position { get; init; }

        public Bookmark ToUpper()
        {
            return new Bookmark()
            {
                Book = Book,
                Chapter = Chapter == 0 ? byte.MaxValue : Chapter,
                Verse = Verse == 0 ? byte.MaxValue : Verse,
                Position = Position == 0 ? byte.MaxValue : Position
            };
        }

        public override string ToString()
        {
            return $"{Book} {Chapter}:{Verse}.{Position}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Bookmark bookmark && Equals(bookmark);
        }

        public bool Equals(Bookmark other)
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
            if (result == 0)
            {
                result = Position.CompareTo(other.Position);
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
