using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public readonly record struct VerbInflectionModel(TenseData Tense, VoiceData Voice, MoodData Mood)
    {
        private static IDictionary<VerbInflectionModel, int>? m_tenseSortOrders;
        private static IDictionary<VerbInflectionModel, int>? m_endingSortOrders;

        public string Description => $"{Tense} - {Voice} - {Mood}";

        public string Anchor => $"{Tense}_{Voice}_{Mood}";

        public string Augment
        {
            get
            {
#pragma warning disable IDE0066 // Convert switch statement to expression
                switch (Mood)
                {
                    case MoodData.Indicative:
                        return Tense switch
                        {
                            TenseData.Present => "-",
                            TenseData.Imperfect => "ε",
                            TenseData.Future => "-",
                            TenseData.Aorist => "ε",
                            TenseData.Perfect => "λε",
                            _ => "-",
                        };
                    default: return "-";
                }
#pragma warning restore IDE0066 // Convert switch statement to expression
            }
        }

        public string TenseFormative
        {
            get
            {
#pragma warning disable IDE0066 // Convert switch statement to expression
                switch (Mood)
                {
                    case MoodData.Indicative:
                        switch (Tense)
                        {
                            case TenseData.Present: return "-";
                            case TenseData.Imperfect: return "-";
                            case TenseData.Future:
                                return Voice switch
                                {
                                    VoiceData.Active => "σ / liquid: εσ",
                                    VoiceData.Middle => "σ / liquid: εσ",
                                    VoiceData.Passive => "1st: θησ / 2nd: ησ",
                                    _ => "-",
                                };
                            case TenseData.Aorist:
                                return Voice switch
                                {
                                    VoiceData.Active => "1st: σα / liquid: α",
                                    VoiceData.Middle => "1st: σα",
                                    VoiceData.Passive => "1st: θη / 2nd: η",
                                    _ => "-",
                                };
                            case TenseData.Perfect:
                                return Voice switch
                                {
                                    VoiceData.Active => "1st: κα / 2nd: α",
                                    VoiceData.Middle => "-",
                                    VoiceData.Passive => "-",
                                    _ => "-",
                                };
                            default: return "-";
                        }
                    default: return "-";
                }
#pragma warning restore IDE0066 // Convert switch statement to expression
            }
        }

        public string ConnectingVowel
        {
            get
            {
#pragma warning disable IDE0066 // Convert switch statement to expression
                switch (Mood)
                {
                    case MoodData.Indicative:
                        switch (Tense)
                        {
                            case TenseData.Present:
                            case TenseData.Imperfect:
                            case TenseData.Future:
                                return "ο / ε";
                            case TenseData.Aorist:
                                switch (Voice)
                                {
                                    case VoiceData.Active:
                                    case VoiceData.Middle:
                                        return "2nd: ο / ε";
                                    case VoiceData.Passive:
                                        return "-";
                                    default: return "-";
                                }
                            default: return "-";
                        }
                    default: return "-";
                }
#pragma warning restore IDE0066 // Convert switch statement to expression
            }
        }

        public PersonalEndingData PersonalEnding
        {
            get
            {
#pragma warning disable IDE0066 // Convert switch statement to expression
                switch (Mood)
                {
                    case MoodData.Indicative:
                        switch (Tense)
                        {
                            case TenseData.Present:
                            case TenseData.Future:
                            case TenseData.Perfect:
                                return Voice == VoiceData.Active ? PersonalEndingData.PrimaryActive : PersonalEndingData.PrimaryPassive;

                            case TenseData.Imperfect:
                                return Voice == VoiceData.Active ? PersonalEndingData.SecondaryActive : PersonalEndingData.SecondaryPassive;

                            case TenseData.Aorist:
                                return Voice == VoiceData.Active || Voice == VoiceData.Passive ? PersonalEndingData.SecondaryActive : PersonalEndingData.SecondaryPassive;

                            default:
                                return PersonalEndingData.None;
                        }

                    default:
                        return PersonalEndingData.None;
                }
#pragma warning restore IDE0066 // Convert switch statement to expression
            }
        }

        public TenseStemData TenseStem
        {
            get
            {
                return Verbs.GetTenseStem(Tense, Voice);
            }
        }

        public override string ToString()
        {
            return $"{Tense} {Voice} {Mood}".Trim();
        }

        public int TenseSortOrder
        {
            get
            {
                return TenseSortOrders.ContainsKey(this) ? TenseSortOrders[this] : 9999;
            }
        }

        public int EndingSortOrder
        {
            get
            {
                return EndingSortOrders.ContainsKey(this) ? EndingSortOrders[this] : 9999;
            }
        }

        private static IDictionary<VerbInflectionModel, int> TenseSortOrders
        {
            get
            {
                int sortOrder = 0;

                m_tenseSortOrders ??= new Dictionary<VerbInflectionModel, int>
                {
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Middle, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Passive, MoodData.Indicative), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Active, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    { new VerbInflectionModel(TenseData.Future, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Future, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Future, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Active, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    { new VerbInflectionModel(TenseData.Pluperfect, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Pluperfect, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    { new VerbInflectionModel(TenseData.Present, VoiceData.Active, MoodData.Imperative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Middle, MoodData.Imperative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Passive, MoodData.Imperative), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Active, MoodData.Imperative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Middle, MoodData.Imperative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Passive, MoodData.Imperative), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Active, MoodData.Imperative), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Present, VoiceData.Active, MoodData.Subjunctive), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Middle, MoodData.Subjunctive), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Passive, MoodData.Subjunctive), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Active, MoodData.Subjunctive), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Middle, MoodData.Subjunctive), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Passive, MoodData.Subjunctive), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Active, MoodData.Subjunctive), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Present, VoiceData.Active, MoodData.Optative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Middle, MoodData.Optative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Passive, MoodData.Optative), ++sortOrder  },

                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Active, MoodData.Optative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Middle, MoodData.Optative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Passive, MoodData.Optative), ++sortOrder  },
                };

                return m_tenseSortOrders;
            }
        }

        private static IDictionary<VerbInflectionModel, int> EndingSortOrders
        {
            get
            {
                int sortOrder = 0;

                m_endingSortOrders ??= new Dictionary<VerbInflectionModel, int>
                {
                    // Primary / Active Voice

                    { new VerbInflectionModel(TenseData.Present, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Future, VoiceData.Active, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Active, MoodData.Indicative), ++sortOrder  },

                    // Primary / Middle Passive Voice

                    { new VerbInflectionModel(TenseData.Present, VoiceData.Middle, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Present, VoiceData.Passive, MoodData.Indicative), ++sortOrder  },
                    { new VerbInflectionModel(TenseData.Future, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Future, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Perfect, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    // Secondary / Active Voice

                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Active, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Active, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },

                    // Secondary / Middle Passive Voice

                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Middle, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Imperfect, VoiceData.Passive, MoodData.Indicative), ++ sortOrder },
                    { new VerbInflectionModel(TenseData.Aorist, VoiceData.Middle, MoodData.Indicative), ++ sortOrder }
                };

                return m_endingSortOrders;
            }
        }
    }
}
