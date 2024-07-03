using BibleCore.Greek.Study;
using BibleCore.Service.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service
{
    public class ExerciseService(IGlobalGreek globalGreek) : IExerciseService
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
