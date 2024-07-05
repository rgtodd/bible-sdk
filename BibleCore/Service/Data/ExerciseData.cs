namespace BibleCore.Service.Data
{
    public class ExerciseData
    {
        public required string CategoryName { get; init; }

        public required string Name { get; init; }

        public required ExerciseQuestionData[] Questions { get; init; }
    }
}
