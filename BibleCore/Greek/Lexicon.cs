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

        public Lexeme GetOrCreateLexeme(string lemma, PartOfSpeech partOfSpeech)
        {
            ArgumentNullException.ThrowIfNull(lemma, nameof(lemma));

            var lexeme = Lexemes.Where(l => l.Lemma == lemma && l.PartOfSpeech == partOfSpeech).FirstOrDefault();
            if (lexeme == null)
            {
                lexeme = new Lexeme
                {
                    Lemma = lemma,
                    PartOfSpeech = partOfSpeech
                };

                Lexemes.Add(lexeme);
            }

            return lexeme;
        }

    }
}
