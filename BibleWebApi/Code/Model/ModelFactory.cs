﻿using BibleCore.Service.Data;

namespace BibleWebApi.Code.Model
{
    public static class ModelFactory
    {
        public static ExerciseModel? CreateExerciseModel(ExerciseVocabularyData? practiceVocabulary)
        {
            return practiceVocabulary == null
                ? null
                : new ExerciseModel()
                {
                    Words = CreateExerciseWordModelArray(practiceVocabulary.Words)
                };
        }

        public static ExerciseWordModel CreateExerciseWordModel(ExerciseWordData practiceWord)
        {
            return new ExerciseWordModel()
            {
                Word = practiceWord.Lemma,
                Options = CreateExerciseWordOptionModelArray(practiceWord.Glosses, practiceWord.Gloss)
            };
        }

        public static ExerciseWordModel[] CreateExerciseWordModelArray(IEnumerable<ExerciseWordData> practiceWords)
        {
            return practiceWords.Select(CreateExerciseWordModel).ToArray();
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