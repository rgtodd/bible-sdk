using BibleCore.Service.Data;

namespace BibleWeb.Models
{
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
