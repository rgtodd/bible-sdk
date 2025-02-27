namespace BibleWebApi.Models
{
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

}
