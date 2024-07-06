namespace BibleCore.Greek
{
    internal class Lexicon
    {
        public List<Lexeme> Lexemes { get; } = [];

        public Dictionary<int, int> MounceChapterWordCount { get; } = [];

        public Lexeme GetOrCreateLexeme(string lemma, PartOfSpeech partOfSpeech)
        {
            ArgumentNullException.ThrowIfNull(lemma, nameof(lemma));

            var lexeme = Lexemes.Where(l => l.Lemma == lemma && l.PartOfSpeech == partOfSpeech).FirstOrDefault();
            if (lexeme == null)
            {
                lexeme = new Lexeme
                {
                    Lemma = lemma,
                    LemmaTransliteration = Alphabet.Transliterate(lemma),
                    PartOfSpeech = partOfSpeech
                };

                Lexemes.Add(lexeme);
            }

            return lexeme;
        }

        public Lexeme GetByStrongsNumber(int strongsNumber)
        {
            var lexeme = Lexemes.Where(l => l.StrongsNumber.Contains(strongsNumber)).FirstOrDefault();
            return lexeme ?? throw new ArgumentOutOfRangeException(nameof(strongsNumber));
        }

        public Lexeme? GetByGkNumber(int gkNumber)
        {
            var lexeme = Lexemes.Where(l => l.GkNumber.Contains(gkNumber)).FirstOrDefault();
            return lexeme; //?? throw new ArgumentOutOfRangeException(nameof(gkNumber));
        }

        public Lexeme GetByLemma(string lemma)
        {
            var lexeme = Lexemes.Where(l => l.Lemma == lemma).FirstOrDefault();
            return lexeme ?? throw new ArgumentOutOfRangeException(nameof(lemma));
        }

        public IEnumerable<Lexeme> GetByMounceChapter(int mounceChapter)
        {
            var lexemes = Lexemes.Where(l => l.MounceChapterNumber == mounceChapter);
            return lexemes;
        }

    }
}
