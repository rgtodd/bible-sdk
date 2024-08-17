namespace BibleCore.Greek
{
    internal class Text
    {
        private List<TextEntry> Entries { get; } = [];

        private Dictionary<Book, Dictionary<byte, Dictionary<byte, int>>> Counts { get; } = [];

        public TextEntry CreateTextEntry(Bookmark bookmark, byte position, string text, string word, string normalizedWord, string transliteratedWord, Lexeme lexeme)
        {
            var textEntry = new TextEntry()
            {
                Bookmark = bookmark,
                Position = position,
                Text = text,
                Word = word,
                NormalizedWord = normalizedWord,
                TransliteratedWord = transliteratedWord,
                Lexeme = lexeme
            };

            Entries.Add(textEntry);

            var bookEntry = Counts.GetValueOrDefault(bookmark.Book);
            if (bookEntry == null)
            {
                bookEntry = [];
                Counts.Add(bookmark.Book, bookEntry);
            }

            var chapterEntry = bookEntry.GetValueOrDefault(bookmark.Chapter);
            if (chapterEntry == null)
            {
                chapterEntry = [];
                bookEntry.Add(bookmark.Chapter, chapterEntry);
            }

            chapterEntry[bookmark.Verse] =
                chapterEntry.TryGetValue(bookmark.Verse, out var wordCount)
                ? wordCount + 1
                : 1;

            return textEntry;
        }

        public Bookmark? GetPrevious(Bookmark bookmark)
        {
            var book = bookmark.Book;
            var chapter = bookmark.Chapter;
            var verse = bookmark.Verse;

            --verse;
            if (verse == 0)
            {
                --chapter;
                if (chapter == 0)
                {
                    if (book == Book.Matthew)
                    {
                        return null;
                    }
                    --book;
                    chapter = ChapterCount(book);
                }
                verse = VerseCount(book, chapter);
            }

            return new Bookmark()
            {
                Book = book,
                Chapter = chapter,
                Verse = verse
            };
        }

        public Bookmark? GetNext(Bookmark bookmark)
        {
            var book = bookmark.Book;
            var chapter = bookmark.Chapter;
            var verse = bookmark.Verse;

            ++verse;
            if (verse > VerseCount(book, chapter))
            {
                verse = 1;
                ++chapter;
                if (chapter > ChapterCount(book))
                {
                    chapter = 1;
                    if (book == Book.Revelation)
                    {
                        return null;
                    }
                    ++book;
                }
            }

            return new Bookmark()
            {
                Book = book,
                Chapter = chapter,
                Verse = verse
            };
        }

        public Range? MovePrevious(Range range)
        {
            var from = GetPrevious(range.From);
            var to = GetPrevious(range.To);

            if (to == null)
            {
                return null;
            }
            if (from == null)
            {
                from = new Bookmark()
                {
                    Book = Book.Matthew,
                    Chapter = 1,
                    Verse = 1
                };
            }

            return new Range()
            {
                From = from.Value,
                To = to.Value
            };
        }

        public Range? MoveNext(Range range)
        {
            var from = GetNext(range.From);
            var to = GetNext(range.To);

            if (from == null)
            {
                return null;
            }
            if (to == null)
            {
                var chapter = ChapterCount(Book.Revelation);
                var verse = VerseCount(Book.Revelation, chapter);

                to = new Bookmark()
                {
                    Book = Book.Revelation,
                    Chapter = chapter,
                    Verse = verse
                };
            }

            return new Range()
            {
                From = from.Value,
                To = to.Value
            };
        }

        public Range? ExtendPrevious(Range range)
        {
            var from = GetPrevious(range.From);

            if (from == null)
            {
                from = new Bookmark()
                {
                    Book = Book.Matthew,
                    Chapter = 1,
                    Verse = 1
                };
            }

            return new Range()
            {
                From = from.Value,
                To = range.To
            };
        }

        public Range? ExtendNext(Range range)
        {
            var to = GetNext(range.To);

            if (to == null)
            {
                var chapter = ChapterCount(Book.Revelation);
                var verse = VerseCount(Book.Revelation, chapter);

                to = new Bookmark()
                {
                    Book = Book.Revelation,
                    Chapter = chapter,
                    Verse = verse
                };
            }

            return new Range()
            {
                From = range.From,
                To = to.Value
            };
        }

        public byte ChapterCount(Book book)
        {
            return (byte)Counts[book].Keys.Count;
        }

        public byte VerseCount(Book book, byte chapter)
        {
            return (byte)Counts[book][chapter].Keys.Count;
        }

        public IEnumerable<TextEntry> Select(Range range, int maxEntries)
        {
            var adjustedRange = new Range()
            {
                From = range.From,
                To = range.To.ToUpperBound()
            };

            int count = 0;
            foreach (var entry in Entries)
            {
                if (adjustedRange.Contains(entry.Bookmark))
                {
                    yield return entry;
                    if (++count == maxEntries)
                    {
                        yield break;
                    }
                }
            }
        }
    }
}
