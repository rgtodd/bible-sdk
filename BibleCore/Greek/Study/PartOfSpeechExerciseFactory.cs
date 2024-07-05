namespace BibleCore.Greek.Study
{
    internal class PartOfSpeechExerciseFactory(Lexicon lexicon, string categoryName, int mounceChapterNumber) : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string CategoryName => categoryName;

        public string Name => $"Chapter {mounceChapterNumber}";

        public Exercise CreateExercise()
        {
            var lexemes = lexicon.GetByMounceChapter(mounceChapterNumber).ToArray();
            Random.Shared.Shuffle(lexemes);

            var allPartsOfSpeech = lexemes.Select(CreatePartOfSpeech).ToArray();

            var questions = new List<Question>();
            foreach (var lexeme in lexemes)
            {
                var correctPartOfSpeech = CreatePartOfSpeech(lexeme);

                Random.Shared.Shuffle(allPartsOfSpeech);
                var possiblePartsOfSpeech = allPartsOfSpeech.Where(g => g != correctPartOfSpeech).Take(AnswerCount - 1).Union([correctPartOfSpeech]).ToArray();
                Random.Shared.Shuffle(possiblePartsOfSpeech);

                var answers = possiblePartsOfSpeech.Select(g => new Answer(g, g == correctPartOfSpeech)).ToArray();

                var detail = new string[] { lexeme.Gloss, lexeme.FullCitationForm };

                var question = new Question(lexeme.Lemma, detail, answers);
                questions.Add(question);
            }

            return new Exercise(CategoryName, Name, [.. questions]);
        }

        private static string CreatePartOfSpeech(Lexeme lexeme)
        {
            var result = lexeme.PartOfSpeech.AsString();
            var nounForm = lexeme.NounForm;
            if (nounForm != null)
            {
                result += " / " + nounForm;
            }
            return result;
        }
    }
}
