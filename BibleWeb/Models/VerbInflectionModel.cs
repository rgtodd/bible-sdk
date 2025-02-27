using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public readonly record struct VerbInflectionModel(MoodData Mood, TenseData Tense, VoiceData Voice)
    {
        private const string PRIMARY_ACTIVE = "Primary / Active";
        private const string PRIMARY_PASSIVE = "Primary / Middle/Passive";
        private const string SECONDARY_ACTIVE = "Secondary / Active";
        private const string SECONDARY_PASSIVE = "Secondary / Middle/Passive";
        private const string OTHER = "Other";

        public string Augment
        {
            get
            {
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
            }
        }

        public string TenseFormative
        {
            get
            {
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
            }
        }

        public string ConnectingVowel
        {
            get
            {
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
            }
        }

        public string Category
        {
            get
            {
                switch (Mood)
                {
                    case MoodData.Indicative:
                        switch (Tense)
                        {
                            case TenseData.Present:
                            case TenseData.Future:
                            case TenseData.Perfect:
                                return Voice == VoiceData.Active ? PRIMARY_ACTIVE : PRIMARY_PASSIVE;

                            case TenseData.Imperfect:
                                return Voice == VoiceData.Active ? SECONDARY_ACTIVE : SECONDARY_PASSIVE;

                            case TenseData.Aorist:
                                return Voice == VoiceData.Active || Voice == VoiceData.Passive ? SECONDARY_ACTIVE : SECONDARY_PASSIVE;

                            default:
                                return OTHER;
                        }

                    default:
                        return OTHER;
                }
            }
        }

        public override string ToString()
        {
            return $"{Mood} {Tense} {Voice}".Trim();
        }
    }

}
