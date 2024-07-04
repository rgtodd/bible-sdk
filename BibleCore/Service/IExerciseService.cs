using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface IExerciseService
    {
        ExerciseCatalogData GetExerciseCatalog();

        ExerciseData GetExercise(string id);
    }
}