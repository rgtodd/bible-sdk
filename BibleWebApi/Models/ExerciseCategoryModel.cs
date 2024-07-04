namespace BibleWebApi.Models
{
    public class ExerciseCategoryModel
    {
        public required string Name { get; init; }

        public required ExerciseCategoryItemModel[] Items { get; init; }
    }
}
