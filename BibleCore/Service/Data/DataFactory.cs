using BibleCore.Greek;
using BibleCore.Greek.Study;

namespace BibleCore.Service.Data
{
    internal static class DataFactory
    {
        public static LexemeData CreateLexemeData(Lexeme lexeme)
        {
            return new LexemeData()
            {
                Lemma = lexeme.Lemma,
                LemmaTransliteration = lexeme.LemmaTransliteration,
                PartOfSpeech = CreatePartOfSpeechData(lexeme.PartOfSpeech),
                PartOfSpeechDescription = lexeme.PartOfSpeech.AsString(),
                FullCitationForm = lexeme.FullCitationForm ?? string.Empty,
                Gloss = lexeme.Gloss ?? string.Empty,
                GkNumber = lexeme.GkNumber,
                StrongsNumber = lexeme.StrongsNumber,
                MounceChapterNumber = lexeme.MounceChapterNumber,
                MounceMorphcat = lexeme.MounceMorphcat,
                Root = lexeme.Root,
                Verbs = lexeme.Verbs,
                Forms = CreateFormDataArray(lexeme.Forms)
            };
        }

        public static TextData? CreateTextData(Greek.Range range, IEnumerable<TextEntry> textEntries, Dictionary<Bookmark, List<ApparatusEntry>> apparatusDictionary)
        {
            var textVerses = new List<TextVerseData>();

            List<TextWordData> currentWords = [];
            Bookmark currentBookmark = Bookmark.Create(Book.Matthew, 1, 1);
            bool initializeBookmark = true;

            foreach (var textEntry in textEntries)
            {
                if (initializeBookmark)
                {
                    currentBookmark = textEntry.Bookmark;
                    initializeBookmark = false;
                }

                if (textEntry.Bookmark != currentBookmark)
                {
                    if (currentWords.Count > 0)
                    {
                        if (currentWords != null)
                        {
                            TextNoteData[] notes = apparatusDictionary.TryGetValue(currentBookmark, out var apparatusEntries)
                                ? CreateTextNodeDataArray(apparatusEntries)
                                : [];

                            var textVerse = new TextVerseData()
                            {
                                Bookmark = CreateBookmarkData(currentBookmark),
                                Words = [.. currentWords],
                                Notes = notes
                            };
                            textVerses.Add(textVerse);
                        }

                        currentWords = [];
                    }

                    currentBookmark = textEntry.Bookmark;
                }

                int? strongsNumber = null;
                if (textEntry.Lexeme.StrongsNumber.Length > 0)
                {
                    strongsNumber = textEntry.Lexeme.StrongsNumber[0];
                }

                int? gkNumber = null;
                if (textEntry.Lexeme.GkNumber.Length > 0)
                {
                    gkNumber = textEntry.Lexeme.GkNumber[0];
                }

                var word = new TextWordData()
                {
                    Position = textEntry.Position,
                    Word = textEntry.Text,
                    Transliteration = textEntry.TransliteratedWord,
                    StrongsNumber = strongsNumber,
                    GkNumber = gkNumber
                };
                currentWords.Add(word);
            }

            if (currentWords.Count > 0)
            {
                TextNoteData[] notes = apparatusDictionary.TryGetValue(currentBookmark, out var apparatusEntries)
                    ? CreateTextNodeDataArray(apparatusEntries)
                    : [];

                var textVerse = new TextVerseData()
                {
                    Bookmark = CreateBookmarkData(currentBookmark),
                    Words = [.. currentWords],
                    Notes = notes
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

        public static ExerciseCatalogData CreateExerciseCatalogData(ExerciseCatalog catalog)
        {
            return new ExerciseCatalogData()
            {
                Factories = CreateExerciseFactoryDataArray(catalog.ExerciseFactories),
                WordLists = CreateThirdPartyWordListDataArray(catalog.ThirdPartyWordLists)
            };
        }

        public static ExerciseData CreateExerciseData(Exercise exercise)
        {
            return new ExerciseData()
            {
                Name = exercise.Name,
                WordListDescription = exercise.WordListDescription,
                WordListId = exercise.WordListId,
                Range = exercise.Range,
                Questions = CreateExerciseQuestionDataArray(exercise.Questions)
            };
        }

        private static ExerciseFactoryData CreateExerciseFactoryData(IExerciseFactory exerciseFactory)
        {
            return new ExerciseFactoryData()
            {
                Name = exerciseFactory.Name
            };
        }

        private static ExerciseFactoryData[] CreateExerciseFactoryDataArray(IEnumerable<IExerciseFactory> exerciseFactories)
        {
            return exerciseFactories.Select(CreateExerciseFactoryData).ToArray();
        }

        private static ThirdPartyWordListData CreateThirdPartyWordListData(ThirdPartyWordList thirdPartyWordList)
        {
            return new ThirdPartyWordListData()
            {
                Name = thirdPartyWordList.Name,
                WordListId = thirdPartyWordList.WordListId
            };
        }

        private static ThirdPartyWordListData[] CreateThirdPartyWordListDataArray(IEnumerable<ThirdPartyWordList> thirdPartyWordLists)
        {
            return thirdPartyWordLists.Select(CreateThirdPartyWordListData).ToArray();
        }

        private static ExerciseQuestionData CreateExerciseQuestionData(Question question)
        {
            return new ExerciseQuestionData()
            {
                Question = question.Text,
                StrongsNumber = question.StrongsNumber,
                GkNumber = question.GkNumber,
                Detail = question.Detail,
                Answers = CreateExerciseAnswerDataArray(question.Answers)
            };
        }

        private static ExerciseQuestionData[] CreateExerciseQuestionDataArray(IEnumerable<Question> questions)
        {
            return questions.Select(CreateExerciseQuestionData).ToArray();
        }

        private static ExerciseAnswerData CreateExerciseAnswerData(Answer answer)
        {
            return new ExerciseAnswerData()
            {
                Answer = answer.Text,
                IsCorrect = answer.IsCorrect,
            };
        }

        private static ExerciseAnswerData[] CreateExerciseAnswerDataArray(IEnumerable<Answer> answers)
        {
            return answers.Select(CreateExerciseAnswerData).ToArray();
        }

        private static BookmarkData CreateBookmarkData(Bookmark bookmark)
        {
            {
                return new BookmarkData()
                {
                    Book = CreateBookData(bookmark.Book),
                    Chapter = bookmark.Chapter,
                    Verse = bookmark.Verse,
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
                InflectedTransliteration = form.InflectedTransliteration,
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

        private static CaseData? CreateCaseData(Case _case)
        {
            return _case switch
            {
                Case.Accusative => CaseData.Accusative,
                Case.Dative => CaseData.Dative,
                Case.Genitive => CaseData.Genitive,
                Case.Nominative => CaseData.Nominative,
                Case.Vocative => CaseData.Vocative,
                Case.Unknown => null,
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

        private static DegreeData? CreateDegreeData(Degree degree)
        {
            return degree switch
            {
                Degree.Comparative => DegreeData.Comparative,
                Degree.Superlative => DegreeData.Superlative,
                Degree.Unknown => null,
                _ => throw new NotImplementedException()
            };
        }

        private static GenderData? CreateGenderData(Gender gender)
        {
            return gender switch
            {
                Gender.Feminine => GenderData.Feminine,
                Gender.Masculine => GenderData.Masculine,
                Gender.Neuter => GenderData.Neuter,
                Gender.Unknown => null,
                _ => throw new NotImplementedException()
            };
        }

        private static MoodData? CreateMoodData(Mood mood)
        {
            return mood switch
            {
                Mood.Imperative => MoodData.Imperative,
                Mood.Indicative => MoodData.Indicative,
                Mood.Infinitive => MoodData.Infinitive,
                Mood.Optative => MoodData.Optative,
                Mood.Participle => MoodData.Participle,
                Mood.Subjunctive => MoodData.Subjunctive,
                Mood.Unknown => null,
                _ => throw new NotImplementedException()
            };
        }

        private static NumberData? CreateNumberData(Number number)
        {
            return number switch
            {
                Number.Plural => NumberData.Plural,
                Number.Singular => NumberData.Singular,
                Number.Unknown => null,
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

        private static PersonData? CreatePersonData(Person person)
        {
            return person switch
            {
                Person.First => PersonData.First,
                Person.Second => PersonData.Second,
                Person.Third => PersonData.Third,
                Person.Unknown => null,
                _ => throw new NotImplementedException()
            };
        }

        private static TenseData? CreateTenseData(Tense tense)
        {
            return tense switch
            {
                Tense.Aorist => TenseData.Aorist,
                Tense.Future => TenseData.Future,
                Tense.Imperfect => TenseData.Imperfect,
                Tense.Perfect => TenseData.Perfect,
                Tense.Pluperfect => TenseData.Pluperfect,
                Tense.Present => TenseData.Present,
                Tense.Unknown => null,
                _ => throw new NotImplementedException()
            };
        }

        private static VoiceData? CreateVoiceData(Voice voice)
        {
            return voice switch
            {
                Voice.Active => VoiceData.Active,
                Voice.Middle => VoiceData.Middle,
                Voice.Passive => VoiceData.Passive,
                Voice.Unknown => null,
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

        private static TextNoteData CreateTextNoteData(ApparatusEntry apparatusEntry)
        {
            return new TextNoteData()
            {
                Note = apparatusEntry.Note
            };
        }

        private static TextNoteData[] CreateTextNodeDataArray(IEnumerable<ApparatusEntry> apparatusEntries)
        {
            return apparatusEntries.Select(CreateTextNoteData).ToArray();
        }
    }
}
