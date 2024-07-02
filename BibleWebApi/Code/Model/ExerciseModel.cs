using BibleCore.Utility;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Code.Model
{
    public class ExerciseModel : IParsable<ExerciseModel>
    {
        public required ExerciseWordModel[] Words {  get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }

    }
}
