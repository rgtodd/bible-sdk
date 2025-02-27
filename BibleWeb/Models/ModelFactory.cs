using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public static class ModelFactory
    {
        private static IDictionary<VerbInflectionModel, int>? m_verbInflectionSortOrder;
        private static IDictionary<string, string>? m_verbCategories;
        private static IDictionary<string, string>? m_verbSubcategories;
        private static IDictionary<string, string>? m_verbMorphology;

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

            string morphology = lexemeData.MounceMorphcat;
            string category = GetVerbCategory(morphology);
            string subcategory = GetVerbSubcategory(morphology);
            string description = GetDescription(morphology);
            string root = lexemeData.Root;
            string verbs = lexemeData.Verbs;

            var verbModel = new VerbModel()
            {
                Tenses = inflections,
                Morphology = morphology,
                Category = category,
                Subcategory = subcategory,
                Description = description,
                Root = root,
                Verbs = verbs
            };

            return verbModel;
        }

        private static int GetVerbInflectionSortOrder(VerbInflectionModel model)
        {
            return VerbInflectionSortOrder.TryGetValue(model, out var result) ? result : 9999;
        }

        private static string GetVerbCategory(string morphology)
        {
            bool isCompound;
            if (morphology.StartsWith('c'))
            {
                isCompound = true;
                morphology = morphology.Substring(1);
            }
            else
            {
                isCompound = false;
            }

            if (morphology.Length < 3)
            {
                return "?";
            }

            morphology = morphology[..3];

            if (VerbCategories.TryGetValue(morphology, out string? category))
            {
                if (isCompound)
                {
                    category += " (Compound)";
                }
                return category;
            }

            return "?";
        }

        private static string GetVerbSubcategory(string morphology)
        {
            if (morphology.StartsWith('c'))
            {
                morphology = morphology.Substring(1);
            }

            if (morphology.Length < 4)
            {
                return "?";
            }

            morphology = morphology[..4];

            if (VerbSubcategories.TryGetValue(morphology, out string? subcategory))
            {
                return subcategory;
            }

            return string.Empty;
        }

        private static string GetDescription(string morphology)
        {
            if (morphology.StartsWith('c'))
            {
                morphology = morphology.Substring(1);
            }

            if (VerbMorphologies.TryGetValue(morphology, out string? description))
            {
                return description;
            }

            return string.Empty;
        }

        private static IDictionary<VerbInflectionModel, int> VerbInflectionSortOrder
        {
            get
            {
                int sortOrder = 0;

                m_verbInflectionSortOrder ??= new Dictionary<VerbInflectionModel, int>
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

                return m_verbInflectionSortOrder;
            }
        }

        private static IDictionary<string, string> VerbCategories
        {
            get
            {
                m_verbCategories ??= new Dictionary<string, string>
                {
                    { "v-1", "Present tense stem = verbal root" },
                    { "v-2", "Present tense stem = verbal root + ι" },
                    { "v-3", "Present tense stem = verbal root + ν" },
                    { "v-4", "Present tense stem = verbal root + τ" },
                    { "v-5", "Present tense stem = verbal root + (ι)σκ" },
                    { "v-6", "Athematic (μι) verbs" },
                    { "v-7", "Verbal roots that undergo ablaut (ἀκούω → ἀκήκοα)" },
                    { "v-8", "Verbs using more than one verbal root" },
                };

                return m_verbCategories;
            }
        }

        private static IDictionary<string, string> VerbSubcategories
        {
            get
            {
                m_verbSubcategories ??= new Dictionary<string, string>
                {
                    { "v-1a", "Roots ending in ι̭ (consonantal iota) or Ϝ (digamma)" },
                    { "v-1b", "Roots ending in a stop" },
                    { "v-1c", "Roots ending in a liquid/nasal" },
                    { "v-1d", "Roots ending in a vowel" },
                    { "v-2a", "Roots ending in δ or γ adds ι̭ > ζω" },
                    { "v-2b", "Roots ending in a velar (κγχ) adds ι̭ > σσω" },
                    { "v-2c", "Roots ending in a Ϝ (digamma)" },
                    { "v-2d", "Roots ending in a liquid (λ ρ) or nasal(μ ν)" },
                };

                return m_verbSubcategories;
            }
        }

        private static IDictionary<string, string> VerbMorphologies
        {
            get
            {
                m_verbMorphology ??= new Dictionary<string, string>
                {
                    { "v-1a(1)", "roots ending in consonantal iota" },
                    { "v-1a(2)", "roots ending in αι" },
                    { "v-1a(3)", "roots ending in ει" },
                    { "v-1a(4)", "roots ending in υ" },
                    { "v-1a(5)", "roots ending in αυ" },
                    { "v-1a(6)", "roots ending in ευ(retain υ in the present)" },
                    { "v-1a(7)", "roots ending in ευ(lose υ in the present)" },
                    { "v-1a(8)", "roots ending in ου" },

                    { "v-1b(1)", "labials(π β φ)" },
                    { "v-1b(2)", "velars(κ γ χ)" },
                    { "v-1b(3)", "dentals(τ δ θ)" },
                    { "v-1b(4)", "stop(adding ε to form the present tense stem)" },

                    { "v-1c(1)", "ρ" },
                    { "v-1c(2)", "μ or ν" },

                    { "v-1d(1a)", "ending in α lengthens before a tense form" },
                    { "v-1d(1b)", "ending in α does not lengthen before a tense form" },
                    { "v-1d(2)", "ε" },
                    { "v-1d(2a)", "ending in ε lengthens before a tense form" },
                    { "v-1d(2b)", "ending in ε does not lengthen before a tense form" },
                    { "v-1d(2c)", "ending in ε loses the ε in the present tense" },
                    { "v-1d(3)", "ending in ο" },

                    { "v-2a(1)", "δ" },
                    { "v-2a(2)", "γ" },

                    { "v-2b", "X" },

                    { "v-2c", "X" },

                    { "v-2d(1)", "λ" },
                    { "v-2d(2)", "αρ" },
                    { "v-2d(3)", "ερ" },
                    { "v-2d(4)", "αν" },
                    { "v-2d(5)", "εν" },

                    { "v-3a(1)", "Roots ending in a vowel add ν" },
                    { "v-3a(2a)", "Roots ending in a consonant add αν(without modifications)" },
                    { "v-3a(2b)", "Roots ending in a consonant add αν(with an epenthetic ν)" },

                    { "v-3b", "Roots adding νε" },

                    { "v-3c(1)", "Roots ending in a vowel add(ν)νυ" },
                    { "v-3c(2)", "Roots ending in a vowel add νυ" },

                    { "v-3d", "Roots adding νι(consonantal iota)" },

                    { "v-4", "X" },

                    { "v-5a", "Roots ending in a vowel adding σκ" },
                    { "v-5b", "Roots ending in a consonant adding ισκ" },

                    { "v-6a", "Roots that reduplicate to form their present tense stem" },
                    { "v-6b", "Roots that do not reduplicate to form their present tense stem" },

                    { "v-7", "X" },

                    { "v-8", "X" },
                };

                return m_verbMorphology;
            }
        }


    }
}
