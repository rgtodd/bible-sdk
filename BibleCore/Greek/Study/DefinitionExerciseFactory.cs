using BibleCore.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class DefinitionExerciseFactory(Lexicon lexicon, int mounceChapterNumber) : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => $"Mounce {mounceChapterNumber}";

        public Exercise CreateExercise()
        {
            var lexemes = lexicon.GetByMounceChapter(mounceChapterNumber).ToArray();
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

                var detail = $"{lexeme.PartOfSpeech} - {lexeme.FullCitationForm}";

                var question = new Question(lexeme.Lemma, detail, answers);
                questions.Add(question);
            }

            return new Exercise([.. questions]);
        }
    }
}
