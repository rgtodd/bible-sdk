using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public static class ModelFactory
    {
        public static ExerciseCatalogModel CreateExerciseCatalogModel(ExerciseCatalogData exerciseCatalog)
        {
            return new ExerciseCatalogModel()
            {
                Factories = CreateExerciseCategoryModelArray(exerciseCatalog.Factories),
                ThirdPartyWordLists = CreateThirdPartyWordListModelArray(exerciseCatalog.WordLists)
            };
        }

        public static ExerciseModel CreateExerciseModel(ExerciseData exercise, bool sort)
        {
            var questions = CreateExerciseWordModelArray(exercise.Questions, sort);

            return new ExerciseModel()
            {
                Name = exercise.Name,
                WordListDescription = exercise.WordListDescription,
                WordListId = exercise.WordListId,
                Range = exercise.Range,
                Questions = questions,
                QuestionsMomento = ExerciseModel.CreateQuestionsMomento(questions)
            };
        }

        private static ExerciseFactoryModel CreateExerciseFactoryModel(ExerciseFactoryData exerciseFactory)
        {
            return new ExerciseFactoryModel()
            {
                Name = exerciseFactory.Name
            };
        }

        private static ExerciseFactoryModel[] CreateExerciseCategoryModelArray(IEnumerable<ExerciseFactoryData> exerciseFactories)
        {
            return exerciseFactories.Select(CreateExerciseFactoryModel).ToArray();
        }

        private static ThirdPartyWordListModel CreateThirdPartyWordListModel(ThirdPartyWordListData thirdPartyWordList)
        {
            return new ThirdPartyWordListModel()
            {
                Name = thirdPartyWordList.Name,
                WordListId = thirdPartyWordList.WordListId
            };
        }

        private static ThirdPartyWordListModel[] CreateThirdPartyWordListModelArray(IEnumerable<ThirdPartyWordListData> thirdPartyWordLists)
        {
            return thirdPartyWordLists.Select(CreateThirdPartyWordListModel).ToArray();
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
