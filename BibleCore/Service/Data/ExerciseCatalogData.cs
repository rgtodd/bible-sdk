namespace BibleCore.Service.Data
{
    public class ExerciseCatalogData
    {
        public required ExerciseFactoryData[] Factories { get; init; }

        public required ThirdPartyWordListData[] WordLists { get; init; }
    }
}
