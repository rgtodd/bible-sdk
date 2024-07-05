using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public static class ModelFactory
    {
        public static ExerciseModel CreateExerciseModel(ExerciseData exercise, bool sort)
        {
            return new ExerciseModel()
            {
                Data = CreateExerciseDataModel(exercise, sort)
            };
        }

        public static ExerciseCatalogModel CreateExerciseCatalogModel(ExerciseCatalogData exerciseCatalog)
        {
            return new ExerciseCatalogModel()
            {
                Categories = CreateExerciseCategoryModelArray(exerciseCatalog.Categories)
            };
        }

        private static ExerciseCategoryModel CreateExerciseCategoryModel(ExerciseCategoryData exerciseCategory)
        {
            return new ExerciseCategoryModel()
            {
                Name = exerciseCategory.Name,
                Items = CreateExerciseCategoryItemModelArray(exerciseCategory.Items)
            };
        }

        private static ExerciseCategoryModel[] CreateExerciseCategoryModelArray(IEnumerable<ExerciseCategoryData> exerciseCategories)
        {
            return exerciseCategories.Select(CreateExerciseCategoryModel).ToArray();
        }

        private static ExerciseCategoryItemModel CreateExerciseCategoryItemModel(ExerciseCategoryItemData exerciseCategoryItem)
        {
            return new ExerciseCategoryItemModel()
            {
                CategoryName = exerciseCategoryItem.CategoryName,
                Name = exerciseCategoryItem.Name,
            };
        }

        private static ExerciseCategoryItemModel[] CreateExerciseCategoryItemModelArray(IEnumerable<ExerciseCategoryItemData> exerciseCategoryItems)
        {
            return exerciseCategoryItems.Select(CreateExerciseCategoryItemModel).ToArray();
        }

        private static ExerciseDataModel CreateExerciseDataModel(ExerciseData exercise, bool sort)
        {
            return new ExerciseDataModel()
            {
                CategoryName = exercise.CategoryName,
                Name = exercise.Name,
                Questions = CreateExerciseWordModelArray(exercise.Questions, sort)
            };
        }

        private static ExerciseQuestionModel CreateExerciseWordModel(ExerciseQuestionData question, int sequence)
        {
            return new ExerciseQuestionModel()
            {
                Sequence = sequence,
                Question = question.Question,
                Detail = question.Detail,
                Answers = CreateExerciseWordOptionModelArray(question.Answers)
            };
        }

        private static ExerciseQuestionModel[] CreateExerciseWordModelArray(IEnumerable<ExerciseQuestionData> questions, bool sort)
        {
            int sequence = 0;

            if (sort)
            {
                questions = questions.OrderBy(q => q.Question);
            }

            return questions.Select(w => CreateExerciseWordModel(w, ++sequence)).ToArray();
        }

        private static ExerciseAnswerModel CreateExerciseWordOptionModel(ExerciseAnswerData answer)
        {
            return new ExerciseAnswerModel()
            {
                Answer = answer.Answer,
                IsCorrect = answer.IsCorrect,
                IsSelected = false
            };
        }

        private static ExerciseAnswerModel[] CreateExerciseWordOptionModelArray(IEnumerable<ExerciseAnswerData> answers)
        {
            return answers.Select(CreateExerciseWordOptionModel).ToArray();
        }
    }
}
