using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Lexicon
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

        public Lexeme GetByStrongs(int strongs)
        {
            var lexeme = Lexemes.Where(l => l.Strongs.Contains(strongs)).FirstOrDefault();
            return lexeme ?? throw new ArgumentOutOfRangeException(nameof(strongs));
        }

        public Lexeme? GetByGkNumber(int gkNumber)
        {
            var lexeme = Lexemes.Where(l => l.Gk.Contains(gkNumber)).FirstOrDefault();
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
