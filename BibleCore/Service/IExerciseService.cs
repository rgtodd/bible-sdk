using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface IExerciseService
    {
        ExerciseData GetExerciseData(string categoryName, string exerciseName);
    }
}