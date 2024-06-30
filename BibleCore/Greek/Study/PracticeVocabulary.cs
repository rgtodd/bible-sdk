using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    public class PracticeVocabulary
    {
        private readonly PracticeWord[] m_words;

        public PracticeVocabulary(PracticeWord[] words)
        {
            m_words = words;
        }

        public static PracticeVocabulary Load(Lexicon lexicon)
        {
            var lexemes = GetLexemes(lexicon);
            var allGlosses = GetAllGlosses(lexemes);
            var allPartsOfSpeech = GetAllPartsOfSpeech(lexemes);

            var words = lexemes.Select(l => CreatePracticeWord(l, allGlosses, allPartsOfSpeech)).ToArray();

            var vocabulary = new PracticeVocabulary(words);

            //vocabulary.AddWord(lexicon, 11);
            //vocabulary.AddWord(lexicon, 32);
            //vocabulary.AddWord(lexicon, 281);
            //vocabulary.AddWord(lexicon, 444);
            //vocabulary.AddWord(lexicon, 652);
            //vocabulary.AddWord(lexicon, 1056);
            //vocabulary.AddWord(lexicon, 1124);
            //vocabulary.AddWord(lexicon, 1138);
            //vocabulary.AddWord(lexicon, 1391);
            //vocabulary.AddWord(lexicon, 1473);
            //vocabulary.AddWord(lexicon, 2078);
            //vocabulary.AddWord(lexicon, 2222);
            //vocabulary.AddWord(lexicon, 2316);
            //vocabulary.AddWord(lexicon, 2532);
            //vocabulary.AddWord(lexicon, 2588);
            //vocabulary.AddWord(lexicon, 2889);
            //vocabulary.AddWord(lexicon, 3056);
            //vocabulary.AddWord(lexicon, 3972);
            //vocabulary.AddWord(lexicon, 4074);
            //vocabulary.AddWord(lexicon, 4091);
            //vocabulary.AddWord(lexicon, 4151);
            //vocabulary.AddWord(lexicon, 4396);
            //vocabulary.AddWord(lexicon, 4521);
            //vocabulary.AddWord(lexicon, 4613);
            //vocabulary.AddWord(lexicon, 5889);
            //vocabulary.AddWord(lexicon, 5547);

            return vocabulary;
        }

        private static List<Lexeme> GetLexemes(Lexicon lexicon)
        {
            var result = new List<Lexeme>
            {
                lexicon.GetByStrongs(11),
                lexicon.GetByStrongs(32),
                lexicon.GetByStrongs(281),
                lexicon.GetByStrongs(444),
                lexicon.GetByStrongs(652),
                lexicon.GetByStrongs(1056),
                lexicon.GetByStrongs(1124),
                lexicon.GetByStrongs(1138),
                lexicon.GetByStrongs(1391),
                lexicon.GetByStrongs(1473),
                lexicon.GetByStrongs(2078),
                lexicon.GetByStrongs(2222),
                lexicon.GetByStrongs(2316),
                lexicon.GetByStrongs(2532),
                lexicon.GetByStrongs(2588),
                lexicon.GetByStrongs(2889),
                lexicon.GetByStrongs(3056),
                lexicon.GetByStrongs(3972),
                lexicon.GetByStrongs(4074),
                lexicon.GetByStrongs(4091),
                lexicon.GetByStrongs(4151),
                lexicon.GetByStrongs(4396),
                lexicon.GetByStrongs(4521),
                lexicon.GetByStrongs(4613),
                lexicon.GetByStrongs(5456),
                lexicon.GetByStrongs(5547)
            };

            return result;
        }

        public static string[] GetAllGlosses(IEnumerable<Lexeme> lexemes)
        {
            return lexemes.Select(l => l.Gloss).ToArray();
        }

        public static PartOfSpeech[] GetAllPartsOfSpeech(IEnumerable<Lexeme> lexemes)
        {
            return lexemes.Select(l => l.PartOfSpeech).Distinct().ToArray();
        }

        public static HashSet<int> GetRandomIndexes(int count, int sourceCount, int requiredIndex)
        {
            var result = new HashSet<int>();

            count = Math.Min(count, sourceCount);
            while (result.Count < count)
            {
                if (Random.Shared.NextDouble() < ((result.Count + 1) / (double)count)
                    && !result.Contains(requiredIndex)
                    )
                {
                    result.Add(requiredIndex);
                }
                else
                {
                    result.Add(Random.Shared.Next(sourceCount));
                }
            }

            return result;
        }

        public PracticeWord[] Words => m_words;

        public static PracticeWord CreatePracticeWord(Lexeme lexeme, string[] allGlosses, PartOfSpeech[] allPartsOfSpeech)
        {
            var requiredGlossIndex = Array.IndexOf(allGlosses, lexeme.Gloss);
            var requiredPartOfSpeechIndex = Array.IndexOf(allPartsOfSpeech, lexeme.PartOfSpeech);

            var glossIndexes = GetRandomIndexes(3, allGlosses.Length, requiredGlossIndex);
            var partsOfSpeechIndexes = GetRandomIndexes(3, allPartsOfSpeech.Length, requiredPartOfSpeechIndex);

            var glosses = glossIndexes.Select(idx => allGlosses[idx]).ToArray();
            var partsOfSpeech = partsOfSpeechIndexes.Select(idx => allPartsOfSpeech[idx]).ToArray();

            var practiceWord = new PracticeWord(lexeme, glosses, partsOfSpeech);

            return practiceWord;
        }
    }
}
