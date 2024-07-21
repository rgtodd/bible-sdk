namespace BibleWebApi.Models
{
    public class ExerciseAnswerModel
    {
        public required string Answer { get; init; }

        public required bool IsCorrect { get; init; }

        public required bool IsSelected { get; set; }

        public string BootstrapButtonClass
        {
            get
            {
                if (IsSelected)
                {
                    if (IsCorrect)
                    {
                        return "btn-success";
                    }
                    else
                    {
                        return "btn-danger";
                    }
                }
                else
                {
                    return "btn-outline-primary";
                }
            }
        }
    }
}
