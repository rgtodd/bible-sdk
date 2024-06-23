using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Lexicon
    {
        private readonly List<Lexeme> m_lexemes = [];

        public Lexeme GetLexeme(string lemma, PartsOfSpeech partOfSpeech)
        {
            ArgumentNullException.ThrowIfNull(lemma, nameof(lemma));

            var lexeme = m_lexemes.Where(l => l.Lemma == lemma && l.PartOfSpeech == partOfSpeech).FirstOrDefault();
            if (lexeme == null)
            {
                lexeme = new Lexeme
                {
                    Lemma = lemma,
                    PartOfSpeech = partOfSpeech
                };

                m_lexemes.Add( lexeme);
            }
            //else
            //{
            //    if (lexeme.PartOfSpeech != partOfSpeech)
            //    {
            //        throw new ArgumentOutOfRangeException(nameof(partOfSpeech), lexeme.PartOfSpeech, "Existing lexeme specifies different part of speech.");
            //    }
            //}

            return lexeme;
        }
    }
}
