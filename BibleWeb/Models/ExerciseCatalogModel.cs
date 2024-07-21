namespace BibleWebApi.Models
{
    public class ExerciseCatalogModel
    {
        public string? Message { get; set; }

        public required ExerciseFactoryModel[] Factories { get; init; }

        public required ThirdPartyWordListModel[] ThirdPartyWordLists { get; init; }

        public string? WordListId { get; set; }

        public string? Range { get; set; }
    }
}
