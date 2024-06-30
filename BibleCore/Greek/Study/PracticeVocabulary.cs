using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    public class PracticeVocabulary
    {
        private readonly List<PracticeWord> m_words;

        public PracticeVocabulary()
        {
            m_words = [];
        }

        public static PracticeVocabulary Load(Lexicon lexicon)
        {

            var vocabulary = new PracticeVocabulary();

            vocabulary.AddWord(lexicon, 11);
            vocabulary.AddWord(lexicon, 32);
            vocabulary.AddWord(lexicon, 281);
            vocabulary.AddWord(lexicon, 444);
            vocabulary.AddWord(lexicon, 652);
            vocabulary.AddWord(lexicon, 1056);
            vocabulary.AddWord(lexicon, 1124);
            vocabulary.AddWord(lexicon, 1138);
            vocabulary.AddWord(lexicon, 1391);
            vocabulary.AddWord(lexicon, 1473);
            vocabulary.AddWord(lexicon, 2078);
            vocabulary.AddWord(lexicon, 2222);
            vocabulary.AddWord(lexicon, 2316);
            vocabulary.AddWord(lexicon, 2532);
            vocabulary.AddWord(lexicon, 2588);
            vocabulary.AddWord(lexicon, 2889);
            vocabulary.AddWord(lexicon, 3056);
            vocabulary.AddWord(lexicon, 3972);
            vocabulary.AddWord(lexicon, 4074);
            vocabulary.AddWord(lexicon, 4091);
            vocabulary.AddWord(lexicon, 4151);
            vocabulary.AddWord(lexicon, 4396);
            vocabulary.AddWord(lexicon, 4521);
            vocabulary.AddWord(lexicon, 4613);
            vocabulary.AddWord(lexicon, 5889);
            vocabulary.AddWord(lexicon, 5547);

            return vocabulary;
        }

        public List<PracticeWord> Words => m_words;

        public void AddWord(Lexicon lexicon, int strongs)
        {
            var lexeme = lexicon.GetByStrongs(strongs);
            if (lexeme != null)
            {
                var practiceWord = new PracticeWord(lexeme);
                Words.Add(practiceWord);
            }
        }
    }
}
