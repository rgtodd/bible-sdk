using BibleCore.Greek;
using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public static class ModelFactory
    {
        public static ExerciseModel CreateExerciseModel(ExerciseData exercise)
        {
            return new ExerciseModel()
            {
                Data = CreateExerciseDataModel(exercise)
            };
        }

        public static ExerciseDataModel CreateExerciseDataModel(ExerciseData exercise)
        {
            return new ExerciseDataModel()
            {
                Questions = CreateExerciseWordModelArray(exercise.Questions)
            };
        }

        public static ExerciseQuestionModel CreateExerciseWordModel(ExerciseQuestionData question, int sequence)
        {
            return new ExerciseQuestionModel()
            {
                Question = question.Question,
                Sequence = sequence,
                Answers = CreateExerciseWordOptionModelArray(question.Answers)
            };
        }

        public static ExerciseQuestionModel[] CreateExerciseWordModelArray(IEnumerable<ExerciseQuestionData> questions)
        {
            int sequence = 0;

            return questions.Select(w => CreateExerciseWordModel(w, ++sequence)).ToArray();
        }

        public static ExerciseAnswerModel CreateExerciseWordOptionModel(ExerciseAnswerData answer)
        {
            return new ExerciseAnswerModel()
            {
                Answer = answer.Answer,
                IsCorrect = answer.IsCorrect,
                IsSelected = false
            };
        }

        public static ExerciseAnswerModel[] CreateExerciseWordOptionModelArray(IEnumerable<ExerciseAnswerData> answers)
        {
            return answers.Select(CreateExerciseWordOptionModel).ToArray();
        }
    }
}
