using BibleCore.Greek.Study;
using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class ExerciseService(IGlobalGreek globalGreek) : IExerciseService
    {
        public ExerciseData GetExerciseData(string categoryName, string exerciseName)
        {
            var exercise = globalGreek.ExerciseCatalog.GetCategory(categoryName).GetExerciseFactory(exerciseName).CreateExercise();
            var exerciseData = DataFactory.CreateExerciseData(exercise);
            return exerciseData;
        }
    }
}
