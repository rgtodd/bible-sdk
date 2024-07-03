using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface IExerciseService
    {
        ExerciseData GetExerciseData();

        ExerciseVocabularyData GetExerciseByMounceChapterNumber(int mounceChapterNumber);
    }
}