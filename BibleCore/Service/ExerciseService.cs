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

        public ExerciseData GetExercise(string categoryName, string name)
        {
            var exercise = globalGreek.ExerciseCatalog.GetCategory(categoryName).GetExerciseFactory(name).CreateExercise();
            var exerciseData = DataFactory.CreateExerciseData(exercise);
            return exerciseData;
        }
    }
}
