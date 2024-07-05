namespace BibleCore.Service.Data
{
    public class ExerciseQuestionData
    {
        public required string Question { get; init; }

        public required string[] Detail { get; init; }

        public required ExerciseAnswerData[] Answers { get; init; }
    }
}
