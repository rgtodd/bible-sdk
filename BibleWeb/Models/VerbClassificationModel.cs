namespace BibleWeb.Models
{
    public class VerbClassificationModel
    {
        public required IList<VerbClassificationCategoryModel> Categories { get; init; }
    }

    public class VerbClassificationCategoryModel
    {
        public required VerbInflectionModel Inflection { get; init; }

        public required IList<VerbClassificationEntryModel> Entries { get; set; }
    }

    public class VerbClassificationEntryModel
    {
        public required string Citation { get; init; }

        public required string Root { get; init; }

        public required string Morphology { get; init; }

        public required string MorphologyCategory { get; init; }

        public required int Strongs { get; init; }

        public required List<string> FirstPersonSingular { get; init; }

        public required List<string> SecondPersonSingular { get; init; }

        public required List<string> ThirdPersonSingular { get; init; }

        public required List<string> FirstPersonPlural { get; init; }

        public required List<string> SecondPersonPlural { get; init; }

        public required List<string> ThirdPersonPlural { get; init; }
    }

}
