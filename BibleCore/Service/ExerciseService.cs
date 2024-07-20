using BibleCore.Greek;
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

        public ExerciseData GetExercise(string name, int? mounceChapterNumber, string? rangeExpression)
        {
            WordList wordList;
            if (mounceChapterNumber.HasValue)
            {
                wordList = WordList.CreateForMounceChapter(globalGreek.Lexicon, mounceChapterNumber.Value);
            }
            else if (rangeExpression != null)
            {
                var range = Greek.Range.Parse(rangeExpression);
                wordList = WordList.CreateForText(globalGreek.Text, range, 100);
            }
            else
            {
                throw new ArgumentException("No word list specified.");
            }

            var exercise = globalGreek.ExerciseCatalog.GetExerciseFactory(name).CreateExercise(wordList);
            var exerciseData = DataFactory.CreateExerciseData(exercise);
            return exerciseData;
        }
    }
}
