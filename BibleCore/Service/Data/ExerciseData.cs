namespace BibleCore.Service.Data
{
    public class ExerciseData
    {
        public required string Name { get; init; }

        public required string WordListDescription { get; init; }

        public required int MounceChapterNumber { get; init; }

        public required ExerciseQuestionData[] Questions { get; init; }
    }
}
