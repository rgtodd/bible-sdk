using BibleCore.Utility;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Models
{
    public class ExerciseAnswerModel : IParsable<ExerciseAnswerModel>
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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseAnswerModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseAnswerModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseAnswerModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }
    }
}
