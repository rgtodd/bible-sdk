using BibleCore.Utility;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Models
{
    public class ExerciseDataModel : IParsable<ExerciseDataModel>
    {
        public required string Name { get; init; }

        public required string WordListDescription { get; init; }

        public required string? WordListId { get; init; }

        public required string? Range { get; init; }

        public required ExerciseQuestionModel[] Questions { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Serialization.JsonSerializerOptions);
        }

        public static ExerciseDataModel Parse(string value, IFormatProvider? provider)
        {
            return !TryParse(value, provider, out var result)
                ? throw new ArgumentException("Could not parse supplied value.", nameof(value))
                : result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out ExerciseDataModel result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = default;
                return false;
            }

            result = JsonSerializer.Deserialize<ExerciseDataModel>(value, Serialization.JsonSerializerOptions);
            return result != null;
        }

    }
}
