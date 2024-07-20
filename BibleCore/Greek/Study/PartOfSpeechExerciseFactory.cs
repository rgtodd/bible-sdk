namespace BibleCore.Greek.Study
{
    internal class PartOfSpeechExerciseFactory() : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => "Parts of Speech";

        public Exercise CreateExercise(WordList wordList)
        {
            var lexemes = wordList.Lexemes;
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

                var detail = new string[] { lexeme.FullCitationForm + " / " + lexeme.PartOfSpeech.AsString(), lexeme.Gloss };

                var question = new Question(lexeme.Lemma, detail, answers);
                questions.Add(question);
            }

            return new Exercise(Name, wordList.Description, wordList.MounceChapterNumber, [.. questions]);
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
