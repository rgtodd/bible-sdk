namespace BibleCore.Service.Data
{
    public class ExerciseCategoryData
    {
        public required string Name { get; init; }

        public required ExerciseCategoryItemData[] Items { get; init; }
    }
}
