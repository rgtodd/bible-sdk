using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public readonly record struct VerbInflectionModel(MoodData Mood, TenseData Tense, VoiceData Voice)
    {
        private static IDictionary<VerbInflectionModel, int>? m_tenseSortOrders;
        private static IDictionary<VerbInflectionModel, int>? m_endingSortOrders;

        public string Description => $"{Mood} - {Tense} - {Voice}";

        public string Anchor => $"{Mood}_{Tense}_{Voice}";

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
            return $"{Mood} {Tense} {Voice}".Trim();
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
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Present, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Active), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Imperfect, VoiceData.Passive), ++ sortOrder },

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Future, VoiceData.Passive), ++ sortOrder },

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Active), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Aorist, VoiceData.Passive), ++ sortOrder },

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Middle), ++ sortOrder },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Perfect, VoiceData.Passive), ++ sortOrder },

                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Pluperfect, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Indicative, TenseData.Pluperfect, VoiceData.Passive), ++ sortOrder },

                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Present, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Present, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Present, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Aorist, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Aorist, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Aorist, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Imperative, TenseData.Perfect, VoiceData.Active), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Present, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Present, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Present, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Aorist, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Aorist, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Aorist, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Subjunctive, TenseData.Perfect, VoiceData.Active), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Optative, TenseData.Present, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Optative, TenseData.Present, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Optative, TenseData.Present, VoiceData.Passive), ++sortOrder  },

                    { new VerbInflectionModel(MoodData.Optative, TenseData.Aorist, VoiceData.Active), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Optative, TenseData.Aorist, VoiceData.Middle), ++sortOrder  },
                    { new VerbInflectionModel(MoodData.Optative, TenseData.Aorist, VoiceData.Passive), ++sortOrder  },
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

                return m_endingSortOrders;
            }
        }
    }
}
