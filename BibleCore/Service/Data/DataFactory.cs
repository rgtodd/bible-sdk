using BibleCore.Greek;
using BibleCore.Greek.Study;

namespace BibleCore.Service.Data
{
    public static class DataFactory
    {
        public static LexemeData CreateLexemeData(Lexeme lexeme)
        {
            return new LexemeData()
            {
                Lemma = lexeme.Lemma,
                LemmaTransliteration = lexeme.LemmaTransliteration,
                PartOfSpeech = CreatePartOfSpeechData(lexeme.PartOfSpeech),
                FullCitationForm = lexeme.FullCitationForm ?? string.Empty,
                Gloss = lexeme.Gloss ?? string.Empty,
                Gk = lexeme.Gk,
                Strongs = lexeme.Strongs,
                Forms = CreateFormDataArray(lexeme.Forms)
            };
        }

        public static TextData? CreateTextData(Greek.Range range, IEnumerable<TextEntry> textEntries)
        {
            var textVerses = new List<TextVerseData>();

            var currentBook = Book.Matthew;
            var currentChapter = (byte)0;
            var currentVerse = (byte)0;
            var currentWords = (List<TextWordData>?)null;

            foreach (var textEntry in textEntries)
            {
                if (currentWords == null
                    || textEntry.Bookmark.Book != currentBook
                    || textEntry.Bookmark.Chapter != currentChapter
                    || textEntry.Bookmark.Verse != currentVerse
                    )
                {
                    if (currentWords != null)
                    {
                        var textVerse = new TextVerseData()
                        {
                            Bookmark = CreateBookmarkData(new Bookmark()
                            {
                                Book = currentBook,
                                Chapter = currentChapter,
                                Verse = currentVerse,
                                Position = 0
                            }),
                            Words = [.. currentWords]
                        };
                        textVerses.Add(textVerse);
                    }

                    currentBook = textEntry.Bookmark.Book;
                    currentChapter = textEntry.Bookmark.Chapter;
                    currentVerse = textEntry.Bookmark.Verse;
                    currentWords = [];
                }

                var word = new TextWordData()
                {
                    Word = textEntry.Text,
                    Strongs = textEntry.Lexeme.Strongs.FirstOrDefault()
                };
                currentWords.Add(word);
            }

            if (currentWords != null)
            {
                var textVerse = new TextVerseData()
                {
                    Bookmark = CreateBookmarkData(
                        new Bookmark()
                        {
                            Book = currentBook,
                            Chapter = currentChapter,
                            Verse = currentVerse,
                            Position = 0
                        }
                    ),
                    Words = [.. currentWords]
                };
                textVerses.Add(textVerse);
            }

            if (textVerses.Count == 0)
            {
                return null;
            }

            var textData = new TextData()
            {
                RangeExpression = range.Format(),
                Verses = [.. textVerses]
            };

            return textData;
        }

        public static ExerciseVocabularyData CreateExerciseVocabularyData(PracticeVocabulary vocabulary)
        {
            return new ExerciseVocabularyData()
            {
                Words = CreateExerciseWordDataArray(vocabulary.Words)
            };
        }

        private static ExerciseWordData[] CreateExerciseWordDataArray(IEnumerable<PracticeWord> practiceWords)
        {
            return practiceWords.Select(CreateExerciseWordData).ToArray();
        }

        private static ExerciseWordData CreateExerciseWordData(PracticeWord practiceWord)
        {
            return new ExerciseWordData()
            {
                Lemma = practiceWord.Lexeme.Lemma,
                Strongs = practiceWord.Lexeme.Strongs[0],
                DefinitionMastery = practiceWord.Masteries[Mastery.Definition],
                PartOfSpeechMastery = practiceWord.Masteries[Mastery.PartOfSpeech],
                Gloss = practiceWord.Lexeme.Gloss ?? string.Empty,
                Glosses = practiceWord.Glosses,
                PartOfSpeech = CreatePartOfSpeechData(practiceWord.Lexeme.PartOfSpeech),
                PartsOfSpeech = practiceWord.PartsOfSpeech.Select(CreatePartOfSpeechData).ToArray()
            };
        }

        private static BookmarkData CreateBookmarkData(Bookmark bookmark)
        {
            {
                return new BookmarkData()
                {
                    Book = CreateBookData(bookmark.Book),
                    Chapter = bookmark.Chapter,
                    Verse = bookmark.Verse,
                    Position = bookmark.Position,
                    FormattedBookmark = bookmark.Format(),
                    FormattedBook = Bookmark.FormatBook(bookmark.Book)
                };
            }
        }

        private static BookmarkData[] CreateBookmarkDataArray(IEnumerable<Bookmark> bookmarks)
        {
            return bookmarks.Select(CreateBookmarkData).ToArray();
        }

        private static FormData CreateFormData(Form form)
        {
            return new FormData()
            {
                InflectedForm = form.InflectedForm,
                Prefix = form.Prefix,
                Core = form.Core,
                Suffix = form.Suffix,
                Inflection = CreateInflectionData(form.Inflection),
                Bookmarks = CreateBookmarkDataArray(form.Bookmarks)
            };
        }

        private static FormData[] CreateFormDataArray(IEnumerable<Form> forms)
        {
            var sortedForms = new List<Form>(forms);
            sortedForms.Sort((l, r) => l.Inflection.CompareTo(r.Inflection));

            return sortedForms.Select(CreateFormData).ToArray();
        }

        private static CaseData? CreateCaseData(Case? _case)
        {
            return _case switch
            {
                null => null,
                Case.Accusative => CaseData.Accusative,
                Case.Dative => CaseData.Dative,
                Case.Genitive => CaseData.Genitive,
                Case.Nominative => CaseData.Nominative,
                Case.Vocative => CaseData.Vocative,
                _ => throw new NotImplementedException()
            };
        }

        private static BookData CreateBookData(Book book)
        {
            return book switch
            {
                Book.Matthew => BookData.Matthew,
                Book.Mark => BookData.Mark,
                Book.Luke => BookData.Luke,
                Book.John => BookData.John,
                Book.Acts => BookData.Acts,
                Book.Romans => BookData.Romans,
                Book.FirstCorinthians => BookData.FirstCorinthians,
                Book.SecondCorinthians => BookData.SecondCorinthians,
                Book.Galatians => BookData.Galatians,
                Book.Ephesians => BookData.Ephesians,
                Book.Philippians => BookData.Philippians,
                Book.Colossians => BookData.Colossians,
                Book.FirstThessalonians => BookData.FirstThessalonians,
                Book.SecondThessalonians => BookData.SecondThessalonians,
                Book.FirstTimothy => BookData.FirstTimothy,
                Book.SecondTimothy => BookData.SecondTimothy,
                Book.Titus => BookData.Titus,
                Book.Philemon => BookData.Philemon,
                Book.Hebrews => BookData.Hebrews,
                Book.James => BookData.James,
                Book.FirstPeter => BookData.FirstPeter,
                Book.SecondPeter => BookData.SecondPeter,
                Book.FirstJohn => BookData.FirstJohn,
                Book.SecondJohn => BookData.SecondJohn,
                Book.ThirdJohn => BookData.ThirdJohn,
                Book.Jude => BookData.Jude,
                Book.Revelation => BookData.Revelation,
                _ => throw new NotImplementedException()
            };
        }

        private static DegreeData? CreateDegreeData(Degree? degree)
        {
            return degree switch
            {
                null => null,
                Degree.Comparative => DegreeData.Comparative,
                Degree.Superlative => DegreeData.Superlative,
                _ => throw new NotImplementedException()
            };
        }

        private static GenderData? CreateGenderData(Gender? gender)
        {
            return gender switch
            {
                null => null,
                Gender.Feminine => GenderData.Feminine,
                Gender.Masculine => GenderData.Masculine,
                Gender.Neuter => GenderData.Neuter,
                _ => throw new NotImplementedException()
            };
        }

        private static MoodData? CreateMoodData(Mood? mood)
        {
            return mood switch
            {
                null => null,
                Mood.Imperative => MoodData.Imperative,
                Mood.Indicative => MoodData.Indicative,
                Mood.Infinitive => MoodData.Infinitive,
                Mood.Optative => MoodData.Optative,
                Mood.Participle => MoodData.Participle,
                Mood.Subjunctive => MoodData.Subjunctive,
                _ => throw new NotImplementedException()
            };
        }

        private static NumberData? CreateNumberData(Number? number)
        {
            return number switch
            {
                null => null,
                Number.Plural => NumberData.Plural,
                Number.Singular => NumberData.Singular,
                _ => throw new NotImplementedException()
            };
        }

        private static PartOfSpeechData CreatePartOfSpeechData(PartOfSpeech partOfSpeech)
        {
            return partOfSpeech switch
            {
                PartOfSpeech.Adjective => PartOfSpeechData.Adjective,
                PartOfSpeech.Adverb => PartOfSpeechData.Adverb,
                PartOfSpeech.Conjunction => PartOfSpeechData.Conjunction,
                PartOfSpeech.DefiniteArticle => PartOfSpeechData.DefiniteArticle,
                PartOfSpeech.DemonstrativePronoun => PartOfSpeechData.DemonstrativePronoun,
                PartOfSpeech.IndefinitePronoun => PartOfSpeechData.IndefinitePronoun,
                PartOfSpeech.Interjection => PartOfSpeechData.Interjection,
                PartOfSpeech.Noun => PartOfSpeechData.Noun,
                PartOfSpeech.Particle => PartOfSpeechData.Particle,
                PartOfSpeech.PersonalPronoun => PartOfSpeechData.PersonalPronoun,
                PartOfSpeech.Preposition => PartOfSpeechData.Preposition,
                PartOfSpeech.RelativePronoun => PartOfSpeechData.RelativePronoun,
                PartOfSpeech.Verb => PartOfSpeechData.Verb,
                _ => throw new NotImplementedException()
            };
        }

        private static PersonData? CreatePersonData(Person? person)
        {
            return person switch
            {
                null => null,
                Person.First => PersonData.First,
                Person.Second => PersonData.Second,
                Person.Third => PersonData.Third,
                _ => throw new NotImplementedException()
            };
        }

        private static TenseData? CreateTenseData(Tense? tense)
        {
            return tense switch
            {
                null => null,
                Tense.Aorist => TenseData.Aorist,
                Tense.Future => TenseData.Future,
                Tense.Imperfect => TenseData.Imperfect,
                Tense.Perfect => TenseData.Perfect,
                Tense.Pluperfect => TenseData.Pluperfect,
                Tense.Present => TenseData.Present,
                _ => throw new NotImplementedException()
            };
        }

        private static VoiceData? CreateVoiceData(Voice? voice)
        {
            return voice switch
            {
                null => null,
                Voice.Active => VoiceData.Active,
                Voice.Middle => VoiceData.Middle,
                Voice.Passive => VoiceData.Passive,
                _ => throw new NotImplementedException()
            };
        }

        private static InflectionData CreateInflectionData(Inflection inflection)
        {
            return new InflectionData
            {
                Case = CreateCaseData(inflection.Case),
                Degree = CreateDegreeData(inflection.Degree),
                Gender = CreateGenderData(inflection.Gender),
                Mood = CreateMoodData(inflection.Mood),
                Number = CreateNumberData(inflection.Number),
                Person = CreatePersonData(inflection.Person),
                Tense = CreateTenseData(inflection.Tense),
                Voice = CreateVoiceData(inflection.Voice)
            };
        }
    }
}
