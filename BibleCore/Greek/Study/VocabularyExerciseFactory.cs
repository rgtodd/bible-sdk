namespace BibleCore.Greek.Study
{
    internal class VocabularyExerciseFactory() : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => "Vocabulary";

        public Exercise CreateExercise(WordList wordList)
        {
            var lexemes = wordList.Lexemes;
            Random.Shared.Shuffle(lexemes);

            var allLemmas = lexemes.Select(l => l.Lemma).ToArray();

            var questions = new List<Question>();
            foreach (var lexeme in lexemes)
            {
                var correctLemma= lexeme.Lemma;

                Random.Shared.Shuffle(allLemmas);
                var possibleLemmas = allLemmas.Where(g => g != correctLemma).Take(AnswerCount - 1).Union([correctLemma]).ToArray();
                Random.Shared.Shuffle(possibleLemmas);

                var answers = possibleLemmas.Select(g => new Answer(g, g == correctLemma)).ToArray();

                var detail = new string[] { lexeme.FullCitationForm + " / " + lexeme.PartOfSpeech.AsString() };

                var question = new Question(lexeme.Gloss, detail, answers);
                questions.Add(question);
            }

            return new Exercise(Name, wordList.Description, wordList.WordListId, wordList.Range, [.. questions]);
        }
    }
}
