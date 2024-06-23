using BibleCore.Properties;

using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.SblGnt
{
    public class SblGntFileParser
    {
        public static Lexicon Parse()
        {
            var lexicon = new Lexicon();

            int lineCount = 0;
            ReadBook(lexicon, "Mt", "_61_Mt_morphgnt", ref lineCount);
            ReadBook(lexicon, "Mk", "_62_Mk_morphgnt", ref lineCount);
            ReadBook(lexicon, "Lk", "_63_Lk_morphgnt", ref lineCount);
            ReadBook(lexicon, "Jn", "_64_Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "Ac", "_65_Ac_morphgnt", ref lineCount);
            ReadBook(lexicon, "Ro", "_66_Ro_morphgnt", ref lineCount);
            ReadBook(lexicon, "1Co", "_67_1Co_morphgnt", ref lineCount);
            ReadBook(lexicon, "2Co", "_68_2Co_morphgnt", ref lineCount);
            ReadBook(lexicon, "Ga", "_69_Ga_morphgnt", ref lineCount);
            ReadBook(lexicon, "Eph", "_70_Eph_morphgnt", ref lineCount);
            ReadBook(lexicon, "Php", "_71_Php_morphgnt", ref lineCount);
            ReadBook(lexicon, "Col", "_72_Col_morphgnt", ref lineCount);
            ReadBook(lexicon, "1Th", "_73_1Th_morphgnt", ref lineCount);
            ReadBook(lexicon, "2Th", "_74_2Th_morphgnt", ref lineCount);
            ReadBook(lexicon, "1Ti", "_75_1Ti_morphgnt", ref lineCount);
            ReadBook(lexicon, "2Ti", "_76_2Ti_morphgnt", ref lineCount);
            ReadBook(lexicon, "Tit", "_77_Tit_morphgnt", ref lineCount);
            ReadBook(lexicon, "Phm", "_78_Phm_morphgnt", ref lineCount);
            ReadBook(lexicon, "Heb", "_79_Heb_morphgnt", ref lineCount);
            ReadBook(lexicon, "Jas", "_80_Jas_morphgnt", ref lineCount);
            ReadBook(lexicon, "1Pe", "_81_1Pe_morphgnt", ref lineCount);
            ReadBook(lexicon, "2Pe", "_82_2Pe_morphgnt", ref lineCount);
            ReadBook(lexicon, "1Jn", "_83_1Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "2Jn", "_84_2Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "3Jn", "_85_3Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "Jud", "_86_Jud_morphgnt", ref lineCount);
            ReadBook(lexicon, "Re", "_87_Re_morphgnt", ref lineCount);

            Console.WriteLine(lineCount + "lines processed.");

            //var sortedInflections = new List<string>(inflections);
            //sortedInflections.Sort();

            foreach (var inflection in LexiconReporter.GetAllInflections(lexicon))
            {
                Console.WriteLine(inflection);
            }

            return lexicon;
        }

        private static void ReadBook(Lexicon lexicon, string bookName, string fileName, ref int lineCount)
        {
            using var stream = Resources.ResourceManager.GetStream(fileName);
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));
            using var reader = new StreamReader(stream);

            //var inflections = new HashSet<string>();

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                ++lineCount;
                string[] fields = line.Split(' ');
                if (fields.Length != 7)
                {
                    Console.WriteLine(line);
                }

                string bookChapterVerse = fields[0];
                string partOfSpeechCode = fields[1];
                string parsingCode = fields[2];
                string text = fields[3];
                string word = fields[4];
                string normalizedWord = fields[5];
                string lemma = fields[6];

                var partOfSpeech = ParsePartOfSpeech(partOfSpeechCode);
                var inflection = new InflectionBuilder().ParseInflection(parsingCode).Build();
                var reference = new Reference() { Value = normalizedWord };

                var lexeme = lexicon.GetOrCreateLexeme(lemma, partOfSpeech);
                var form = lexeme.GetOrCreateForm(normalizedWord, inflection);
                form.References.Add(reference);

                //inflections.Add(partOfSpeech + '-' + inflection);

                //                *book / chapter / verse
                //* part of speech
                // *parsing code
                // * text(including punctuation)
                // *word(with punctuation stripped)
                // * normalized word
                // * lemma
            }
        }

        private static PartsOfSpeech ParsePartOfSpeech(string value)
        {
            return value switch
            {
                "A-" => PartsOfSpeech.Adjective,
                "C-" => PartsOfSpeech.Conjunction,
                "D-" => PartsOfSpeech.Adverb,
                "I-" => PartsOfSpeech.Interjection,
                "N-" => PartsOfSpeech.Noun,
                "P-" => PartsOfSpeech.Preposition,
                "RA" => PartsOfSpeech.DefiniteArticle,
                "RD" => PartsOfSpeech.DemonstrativePronoun,
                "RI" => PartsOfSpeech.IndefinitePronoun,
                "RP" => PartsOfSpeech.PersonalPronoun,
                "RR" => PartsOfSpeech.RelativePronoun,
                "V-" => PartsOfSpeech.Verb,
                "X-" => PartsOfSpeech.Particle,
                _ => throw new NotImplementedException()
            };
        }
    }
}
