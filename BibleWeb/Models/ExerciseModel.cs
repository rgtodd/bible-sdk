using BibleCore.Utility;

using System.Text.Json;

namespace BibleWeb.Models
{
    public class ExerciseModel
    {
        public required string Name { get; init; }

        public required string WordListDescription { get; init; }

        public required string? WordListId { get; init; }

        public required string? Range { get; init; }

        public required ExerciseQuestionModel[] Questions { get; init; }

        public required string QuestionsMomento { get; init; }


        public static string CreateQuestionsMomento(ExerciseQuestionModel[] questions)
        {
            var result = JsonSerializer.Serialize(questions, Serialization.JsonSerializerOptions);
            return result;
        }

        public static ExerciseQuestionModel[] RestoreQuestionsMomento(string questionsMomento)
        {
            var result = JsonSerializer.Deserialize<ExerciseQuestionModel[]>(questionsMomento, Serialization.JsonSerializerOptions);
            return result ?? throw new ArgumentNullException(nameof(questionsMomento));
        }
    }
}
