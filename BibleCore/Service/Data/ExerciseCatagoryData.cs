namespace BibleCore.Service.Data
{
    public class ExerciseCatagoryData
    {
        public required string Name { get; init; }

        public required ExerciseCategoryItemData[] Items { get; init; }
    }
}
