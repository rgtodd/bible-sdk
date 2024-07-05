namespace BibleCore.Greek.Study
{
    internal class PronounciationExerciseFactory(Lexicon lexicon, string categoryName, int mounceChapterNumber) : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string CategoryName => categoryName;

        public string Name => $"Chapter {mounceChapterNumber}";

        public Exercise CreateExercise()
        {
            var lexemes = lexicon.GetByMounceChapter(mounceChapterNumber).ToArray();
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

                var detail = new string[] { lexeme.FullCitationForm, lexeme.Gloss };

                var question = new Question(lexeme.Lemma, detail, answers);
                questions.Add(question);
            }

            return new Exercise(CategoryName, Name, [.. questions]);
        }
    }
}
