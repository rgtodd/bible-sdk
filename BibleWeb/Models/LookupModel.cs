using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public class LookupModel
    {
        public required string? Message { get; init; }

        public required LexemeData? LexemeData { get; init; }

        public required int? StrongsNumber { get; init; }

        public required int? GkNumber { get; init; }

        public required string? Range { get; init; }

        public required VerbModel? Verb { get; init; }
    }

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

    public class VerbModel
    {
        public required string Morphology { get; init; }

        public required string Category { get; init; }

        public required string Subcategory { get; init; }

        public required string Description { get; init; }

        public required string Root { get; init; }

        public required string Verbs { get; init; }

        public required IList<VerbTenseModel> Tenses { get; init; }
    }

    public class VerbTenseModel
    {
        public required VerbInflectionModel Inflection { get; init; }

        public required IList<FormData> FirstPersonSingular { get; init; }

        public required IList<FormData> SecondPersonSingular { get; init; }

        public required IList<FormData> ThirdPersonSingular { get; init; }

        public required IList<FormData> FirstPersonPlural { get; init; }

        public required IList<FormData> SecondPersonPlural { get; init; }

        public required IList<FormData> ThirdPersonPlural { get; init; }
    }

}
