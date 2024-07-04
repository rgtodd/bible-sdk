using BibleCore.Greek.Study;
using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class ExerciseService(IGlobalGreek globalGreek) : IExerciseService
    {
        public ExerciseVocabularyData GetExerciseByMounceChapterNumber(int mounceChapterNumber)
        {
            var practiceVocabulary = PracticeVocabulary.Load(globalGreek.Lexicon, mounceChapterNumber);
            var exerciseVocabularyData = DataFactory.CreateExerciseVocabularyData(practiceVocabulary);

            return exerciseVocabularyData;
        }

        public ExerciseData GetExerciseData()
        {
            return new ExerciseData()
            {
                MounceChapterWordCount = globalGreek.Lexicon.MounceChapterWordCount
            };
        }
    }
}
