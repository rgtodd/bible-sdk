using BibleCore.Utility;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Code.Model
{
    public class ExerciseWordModel : IParsable<ExerciseWordModel>
    {
        public required string Word { get; init; }

        public required ExerciseWordOptionModel[] Options { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseWordModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseWordModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseWordModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }
    }
}
