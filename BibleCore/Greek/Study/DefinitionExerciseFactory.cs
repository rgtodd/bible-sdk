﻿namespace BibleCore.Greek.Study
{
    internal class DefinitionExerciseFactory() : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => "Definitions";

        public Exercise CreateExercise(WordList wordList)
        {
            var lexemes = wordList.Lexemes;
            Random.Shared.Shuffle(lexemes);

            var allGlosses = lexemes.Select(l => l.Gloss).ToArray();

            var questions = new List<Question>();
            foreach (var lexeme in lexemes)
            {
                var correctGloss = lexeme.Gloss;

                Random.Shared.Shuffle(allGlosses);
                var possibleGlosses = allGlosses.Where(g => g != correctGloss).Take(AnswerCount - 1).Union([correctGloss]).ToArray();
                Random.Shared.Shuffle(possibleGlosses);

                var answers = possibleGlosses.Select(g => new Answer(g, g == correctGloss)).ToArray();

                var detail = new string[] { lexeme.FullCitationForm + " / " + lexeme.PartOfSpeech.AsString() };

                var question = new Question(lexeme.Lemma, detail, answers, First(lexeme.StrongsNumber), First(lexeme.GkNumber));
                questions.Add(question);
            }

            return new Exercise(Name, wordList.Description, wordList.WordListId, wordList.Range, [.. questions]);
        }

        private int? First(int[] values) => values.Length > 0 ? values[0] : null;
    }
}
