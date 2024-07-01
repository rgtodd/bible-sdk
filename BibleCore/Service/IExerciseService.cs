using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface IExerciseService
    {
        ExerciseVocabularyData GetExerciseByMounceChapterNumber(int mounceChapterNumber);
    }
}