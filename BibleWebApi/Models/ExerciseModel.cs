using BibleCore.Utility;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BibleWebApi.Models
{
    public class ExerciseModel
    {
        public required ExerciseDataModel Data { get; init; }
    }
}
