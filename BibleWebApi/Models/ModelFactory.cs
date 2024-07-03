using BibleCore.Greek;
using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public static class ModelFactory
    {
        public static ExerciseCatagoryListModel CreateExerciseCatagoryListModel(Lexicon lexicon)
        {
            var exerciseCategoryItems = new List<ExerciseCategoryItemModel>();
            foreach (var chapter in lexicon.MounceChapterWordCount.Keys.Order())
            {
                exerciseCategoryItems.Add(new ExerciseCategoryItemModel()
                {
                    Name = $"Chapter {chapter} ({lexicon.MounceChapterWordCount[chapter]} words)"
                });
            }

            var exerciseCategories = new List<ExerciseCatagoryModel>();
            var exerciseCategory = new ExerciseCatagoryModel()
            {
                Name = "Definitions",
                Items = [.. exerciseCategoryItems]
            };
            exerciseCategories.Add(exerciseCategory);

            var exerciseCategoryList = new ExerciseCatagoryListModel()
            {
                Categories = [.. exerciseCategories]
            };

            return exerciseCategoryList;
        }

        public static ExerciseModel CreateExerciseModel(ExerciseVocabularyData practiceVocabulary)
        {
            return new ExerciseModel()
            {
                Data = CreateExerciseDataModel(practiceVocabulary)
            };
        }

        public static ExerciseDataModel CreateExerciseDataModel(ExerciseVocabularyData practiceVocabulary)
        {
            return new ExerciseDataModel()
            {
                Words = CreateExerciseWordModelArray(practiceVocabulary.Words)
            };
        }

        public static ExerciseWordModel CreateExerciseWordModel(ExerciseWordData practiceWord, int sequence)
        {
            return new ExerciseWordModel()
            {
                Word = practiceWord.Lemma,
                Sequence = sequence,
                Options = CreateExerciseWordOptionModelArray(practiceWord.Glosses, practiceWord.Gloss)
            };
        }

        public static ExerciseWordModel[] CreateExerciseWordModelArray(IEnumerable<ExerciseWordData> practiceWords)
        {
            int sequence = 0;

            return practiceWords.Select(w => CreateExerciseWordModel(w, ++sequence)).ToArray();
        }

        public static ExerciseWordOptionModel CreateExerciseWordOptionModel(string option, bool isCorrect)
        {
            return new ExerciseWordOptionModel()
            {
                Option = option,
                IsCorrect = isCorrect,
                IsSelected = false
            };
        }

        public static ExerciseWordOptionModel[] CreateExerciseWordOptionModelArray(IEnumerable<string> options, string correctOption)
        {
            return options.Select(option => CreateExerciseWordOptionModel(option, option == correctOption)).ToArray();
        }
    }
}
