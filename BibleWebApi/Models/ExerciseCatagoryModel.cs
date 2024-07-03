namespace BibleWebApi.Models
{
    public class ExerciseCatagoryModel
    {
        public required string Name { get; init; }

        public required ExerciseCategoryItemModel[] Items { get; init; }
    }
}
