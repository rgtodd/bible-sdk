namespace BibleWebApi.Models
{
    public class ExerciseQuestionModel
    {
        public required int Sequence { get; init; }

        public required string Question { get; init; }

        public required string[] Detail { get; init; }

        public required ExerciseAnswerModel[] Answers { get; init; }

        public bool IsCorrect
        {
            get
            {
                foreach (var answer in Answers)
                {
                    if (answer.IsSelected && answer.IsCorrect)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
