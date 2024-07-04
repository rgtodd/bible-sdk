using BibleCore.Greek.Study;
using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class ExerciseService(IGlobalGreek globalGreek) : IExerciseService
    {
        public ExerciseCatalogData GetExerciseCatalog()
        {
            var exerciseCatalogData = DataFactory.CreateExerciseCatalogData(globalGreek.ExerciseCatalog);
            return exerciseCatalogData;
        }

        public ExerciseData GetExercise(string id)
        {
            var fields = id.Split('-');
            string categoryName = fields[0];
            string exerciseName = fields[1];

            var exercise = globalGreek.ExerciseCatalog.GetCategory(categoryName).GetExerciseFactory(exerciseName).CreateExercise();
            var exerciseData = DataFactory.CreateExerciseData(exercise);
            return exerciseData;
        }
    }
}
