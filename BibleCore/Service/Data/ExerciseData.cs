namespace BibleCore.Service.Data
{
    public class ExerciseData
    {
        public required string Name { get; init; }

        public required string WordListDescription { get; init; }

        public required string? WordListId { get; init; }

        public required string? Range { get; init; }

        public required ExerciseQuestionData[] Questions { get; init; }
    }
}
