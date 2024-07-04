using BibleCore.Utility;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Models
{
    public class ExerciseQuestionModel : IParsable<ExerciseQuestionModel>
    {
        public required int Sequence { get; init; }

        public required string Question { get; init; }

        public required string Detail { get; init; }

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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseQuestionModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseQuestionModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseQuestionModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }
    }
}
