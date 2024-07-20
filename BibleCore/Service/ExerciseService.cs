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

        public ExerciseData GetExercise(string name, string? wordListId, string? rangeExpression)
        {
            WordList wordList;
            if (wordListId != null)
            {
                var mounceChapterNumber = int.Parse(wordListId[2..]); // HACK
                wordList = WordList.CreateForMounceChapter(globalGreek.Lexicon, mounceChapterNumber);
            }
            else if (rangeExpression != null)
            {
                var range = Greek.Range.Parse(rangeExpression);
                wordList = WordList.CreateForText(globalGreek.Text, range, 100);
            }
            else
            {
                throw new ArgumentException("A word list or scripture reference must be specified.");
            }

            var exercise = globalGreek.ExerciseCatalog.GetExerciseFactory(name).CreateExercise(wordList);
            var exerciseData = DataFactory.CreateExerciseData(exercise);
            return exerciseData;
        }
    }
}
