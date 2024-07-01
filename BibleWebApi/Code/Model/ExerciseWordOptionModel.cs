using BibleCore.Utility;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Code.Model
{
    public class ExerciseWordOptionModel : IParsable<ExerciseWordOptionModel>
    {
        public required string Option { get; init; }

        public required bool IsCorrect { get; init; }

        public required bool IsSelected { get; init; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseWordOptionModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseWordOptionModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseWordOptionModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }
    }
}
