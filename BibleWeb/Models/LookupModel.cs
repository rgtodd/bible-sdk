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

    public readonly record struct VerbInflectionModel(MoodData? Mood, TenseData? Tense, VoiceData? Voice)
    {
        private const string PRIMARY_ACTIVE = "Primary / Active";
        private const string PRIMARY_PASSIVE = "Primary / Middle/Passive";
        private const string SECONDARY_ACTIVE = "Secondary / Active";
        private const string SECONDARY_PASSIVE = "Secondary / Middle/Passive";
        private const string OTHER = "Other";

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
        public required IList<VerbTenseModel> Inflections { get; init; }
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

        public string VerbInflection
        {
            get
            {
                return Inflection.ToString();
            }
        }



    }

}
