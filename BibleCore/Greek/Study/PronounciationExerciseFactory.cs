namespace BibleCore.Greek.Study
{
    internal class PronounciationExerciseFactory() : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => "Pronounciations";

        public Exercise CreateExercise(WordList wordList)
        {
            var lexemes = wordList.Lexemes;
            Random.Shared.Shuffle(lexemes);

            var allTransliterations = lexemes.Select(l => l.LemmaTransliteration).ToArray();

            var questions = new List<Question>();
            foreach (var lexeme in lexemes)
            {
                var correctTransliteration = lexeme.LemmaTransliteration;

                Random.Shared.Shuffle(allTransliterations);
                var possibleTransliterations = allTransliterations.Where(g => g != correctTransliteration).Take(AnswerCount - 1).Union([correctTransliteration]).ToArray();
                Random.Shared.Shuffle(possibleTransliterations);

                var answers = possibleTransliterations.Select(g => new Answer(g, g == correctTransliteration)).ToArray();

                var detail = new string[] { lexeme.FullCitationForm + " / " + lexeme.PartOfSpeech.AsString(), lexeme.Gloss };

                var question = new Question(lexeme.Lemma, detail, answers);
                questions.Add(question);
            }

            return new Exercise(Name, wordList.Description, wordList.MounceChapterNumber, [.. questions]);
        }
    }
}
