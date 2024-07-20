namespace BibleWebApi.Models
{
    public class ExerciseCatalogModel
    {
        public required ExerciseFactoryModel[] Factories { get; init; }

        public required ThirdPartyWordListModel[] ThirdPartyWordLists { get; init; }
    }
}
