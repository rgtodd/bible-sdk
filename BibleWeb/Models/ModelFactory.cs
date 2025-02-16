using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TextTemplating;


namespace BibleWebApi.Models
{
    public static class ModelFactory
    {
        private static IDictionary<VerbInflectionModel, int>? verbInflectionSortOrder;

        public static LookupModel CreateLookupModel(LexemeData? lexemeData, int? strongs, int? gk, string? range)
        {
            VerbModel? verbModel = null;

            if (lexemeData != null)
            {
                if (lexemeData.StrongsNumber != null && lexemeData.StrongsNumber.Length > 0)
                {
                    strongs = lexemeData.StrongsNumber[0];
                }

                if (lexemeData.GkNumber != null && lexemeData.GkNumber.Length > 0)
                {
                    gk = lexemeData.GkNumber[0];
                }

                if (lexemeData.PartOfSpeech == PartOfSpeechData.Verb)
                {
                    verbModel = CreateVerbModel(lexemeData);
                }
            }

            var message = (strongs != null || gk != null) && lexemeData == null ? "Not found." : null;

            var model = new LookupModel()
            {
                Message = message,
                LexemeData = lexemeData,
                StrongsNumber = strongs,
                GkNumber = gk,
                Range = range,
                Verb = verbModel
            };

            return model;
        }

        public static ExerciseCatalogModel CreateExerciseCatalogModel(ExerciseCatalogData exerciseCatalog, string? wordListId, string? range)
        {
            return new ExerciseCatalogModel()
            {
                Factories = CreateExerciseFactoryModelArray(exerciseCatalog.Factories),
                ThirdPartyWordLists = CreateThirdPartyWordListModelArray(exerciseCatalog.WordLists),
                WordListId = wordListId,
                Range = range
            };
        }

        public static ExerciseModel CreateExerciseModel(ExerciseData exercise, bool sort)
        {
            var questions = CreateExerciseQuestionModelArray(exercise.Questions, sort);

            return new ExerciseModel()
            {
                Name = exercise.Name,
                WordListDescription = exercise.WordListDescription,
                WordListId = exercise.WordListId,
                Range = exercise.Range,
                Questions = questions,
                QuestionsMomento = ExerciseModel.CreateQuestionsMomento(questions)
            };
        }

        public static LexemeListModel CreateLexemeListModel(List<LexemeData> lexemes)
        {
            var sortedLexemes = lexemes
                .OrderBy(l => l.PartOfSpeechDescription)
                //.ThenBy(l => l.MounceMorphcat)
                .ThenBy(l => l.FullCitationForm)
                .ToList();

            return new LexemeListModel()
            {
                Lexemes = sortedLexemes
            };
        }

        private static ExerciseFactoryModel CreateExerciseFactoryModel(ExerciseFactoryData exerciseFactory)
        {
            return new ExerciseFactoryModel()
            {
                Name = exerciseFactory.Name
            };
        }

        private static ExerciseFactoryModel[] CreateExerciseFactoryModelArray(IEnumerable<ExerciseFactoryData> exerciseFactories)
        {
            return exerciseFactories.Select(CreateExerciseFactoryModel).ToArray();
        }

        private static ThirdPartyWordListModel CreateThirdPartyWordListModel(ThirdPartyWordListData thirdPartyWordList)
        {
            return new ThirdPartyWordListModel()
            {
                Name = thirdPartyWordList.Name,
                WordListId = thirdPartyWordList.WordListId
            };
        }

        private static ThirdPartyWordListModel[] CreateThirdPartyWordListModelArray(IEnumerable<ThirdPartyWordListData> thirdPartyWordLists)
        {
            return thirdPartyWordLists.Select(CreateThirdPartyWordListModel).ToArray();
        }

        private static ExerciseQuestionModel CreateExerciseQuestionModel(ExerciseQuestionData question, int sequence)
        {
            return new ExerciseQuestionModel()
            {
                Sequence = sequence,
                Question = question.Question,
                StrongsNumber = question.StrongsNumber,
                GkNumber = question.GkNumber,
                Detail = question.Detail,
                Answers = CreateExerciseAnswerModelArray(question.Answers)
            };
        }

        private static ExerciseQuestionModel[] CreateExerciseQuestionModelArray(IEnumerable<ExerciseQuestionData> questions, bool sort)
        {
            int sequence = 0;

            if (sort)
            {
                questions = questions.OrderBy(q => q.Question);
            }

            return questions.Select(w => CreateExerciseQuestionModel(w, ++sequence)).ToArray();
        }

        private static ExerciseAnswerModel CreateExerciseAnswerModel(ExerciseAnswerData answer)
        {
            return new ExerciseAnswerModel()
            {
                Answer = answer.Answer,
                IsCorrect = answer.IsCorrect,
                IsSelected = false
            };
        }

        private static ExerciseAnswerModel[] CreateExerciseAnswerModelArray(IEnumerable<ExerciseAnswerData> answers)
        {
            return answers.Select(CreateExerciseAnswerModel).ToArray();
        }

        private static VerbModel CreateVerbModel(LexemeData lexemeData)
        {
            var inflections = new List<VerbTenseModel>();

            VerbTenseModel? currentInflection = null;
            MoodData currentMood = default;
            TenseData currentTense = default;
            VoiceData currentVoice = default;
            foreach (var form in lexemeData.Forms.Where(
                    form => form.Inflection.Mood != null
                    && form.Inflection.Tense != null
                    && form.Inflection.Voice != null
                    && form.Inflection.Person != null
                    && form.Inflection.Number != null))
            {
                if (currentInflection == null
                    || form.Inflection.Mood != currentMood
                    || form.Inflection.Tense != currentTense
                    || form.Inflection.Voice != currentVoice)
                {
                    currentMood = form.Inflection.Mood.GetValueOrDefault();
                    currentTense = form.Inflection.Tense.GetValueOrDefault();
                    currentVoice = form.Inflection.Voice.GetValueOrDefault();

                    currentInflection = new VerbTenseModel()
                    {
                        Inflection = new VerbInflectionModel(currentMood, currentTense, currentVoice),
                        FirstPersonSingular = [],
                        SecondPersonSingular = [],
                        ThirdPersonSingular = [],
                        FirstPersonPlural = [],
                        SecondPersonPlural = [],
                        ThirdPersonPlural = []
                    };
                    inflections.Add(currentInflection);
                }

                switch (form.Inflection.Person)
                {
                    case PersonData.First:
                        switch (form.Inflection.Number)
                        {
                            case NumberData.Singular:
                                currentInflection.FirstPersonSingular.Add(form);
                                break;

                            case NumberData.Plural:
                                currentInflection.FirstPersonPlural.Add(form);
                                break;
                        }
                        break;

                    case PersonData.Second:
                        switch (form.Inflection.Number)
                        {
                            case NumberData.Singular:
                                currentInflection.SecondPersonSingular.Add(form);
                                break;

                            case NumberData.Plural:
                                currentInflection.SecondPersonPlural.Add(form);
                                break;
                        }
                        break;

                    case PersonData.Third:
                        switch (form.Inflection.Number)
                        {
                            case NumberData.Singular:
                                currentInflection.ThirdPersonSingular.Add(form);
                                break;

                            case NumberData.Plural:
                                currentInflection.ThirdPersonPlural.Add(form);
                                break;
                        }
                        break;
                }
            }

            inflections.Sort(Comparer<VerbTenseModel>.Create((lhs, rhs) => GetVerbInflectionSortOrder(lhs.Inflection).CompareTo(GetVerbInflectionSortOrder(rhs.Inflection))));

            var verbModel = new VerbModel()
            {
                Tenses = inflections
            };

            return verbModel;
        }

        private static int GetVerbInflectionSortOrder(VerbInflectionModel model)
        {
            int sortOrder = 0;
            verbInflectionSortOrder ??= new Dictionary<VerbInflectionModel, int>
                {
                    // Primary / Active Voice

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Active), ++sortOrder  },

                    // Primary / Middle Passive Voice

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Passive), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Passive), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Passive), ++ sortOrder },

                    // Secondary / Active Voice

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Active), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Active), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Passive), ++ sortOrder },

                    // Secondary / Middle Passive Voice

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Passive), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Middle), ++ sortOrder }
                };

            return verbInflectionSortOrder.TryGetValue(model, out var result) ? result : 9999;
        }
    }
}
