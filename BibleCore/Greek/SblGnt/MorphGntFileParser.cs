using BibleCore.Properties;

using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.SblGnt
{
    public class MorphGntFileParser
    {
        public static void Parse(Lexicon lexicon)
        {
            int lineCount = 0;

            ReadBook(lexicon, "_61_Mt_morphgnt", ref lineCount);
            ReadBook(lexicon, "_62_Mk_morphgnt", ref lineCount);
            ReadBook(lexicon, "_63_Lk_morphgnt", ref lineCount);
            ReadBook(lexicon, "_64_Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "_65_Ac_morphgnt", ref lineCount);
            ReadBook(lexicon, "_66_Ro_morphgnt", ref lineCount);
            ReadBook(lexicon, "_67_1Co_morphgnt", ref lineCount);
            ReadBook(lexicon, "_68_2Co_morphgnt", ref lineCount);
            ReadBook(lexicon, "_69_Ga_morphgnt", ref lineCount);
            ReadBook(lexicon, "_70_Eph_morphgnt", ref lineCount);
            ReadBook(lexicon, "_71_Php_morphgnt", ref lineCount);
            ReadBook(lexicon, "_72_Col_morphgnt", ref lineCount);
            ReadBook(lexicon, "_73_1Th_morphgnt", ref lineCount);
            ReadBook(lexicon, "_74_2Th_morphgnt", ref lineCount);
            ReadBook(lexicon, "_75_1Ti_morphgnt", ref lineCount);
            ReadBook(lexicon, "_76_2Ti_morphgnt", ref lineCount);
            ReadBook(lexicon, "_77_Tit_morphgnt", ref lineCount);
            ReadBook(lexicon, "_78_Phm_morphgnt", ref lineCount);
            ReadBook(lexicon, "_79_Heb_morphgnt", ref lineCount);
            ReadBook(lexicon, "_80_Jas_morphgnt", ref lineCount);
            ReadBook(lexicon, "_81_1Pe_morphgnt", ref lineCount);
            ReadBook(lexicon, "_82_2Pe_morphgnt", ref lineCount);
            ReadBook(lexicon, "_83_1Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "_84_2Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "_85_3Jn_morphgnt", ref lineCount);
            ReadBook(lexicon, "_86_Jud_morphgnt", ref lineCount);
            ReadBook(lexicon, "_87_Re_morphgnt", ref lineCount);

            Console.WriteLine(lineCount + "lines processed.");
        }

        private static void ReadBook(Lexicon lexicon, string fileName, ref int lineCount)
        {
            var bookText = Resources.ResourceManager.GetString(fileName);
            ArgumentNullException.ThrowIfNull(bookText, nameof(bookText));

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
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
                var reference = new Reference() { Value = bookChapterVerse };

                var lexeme = lexicon.GetOrCreateLexeme(lemma, partOfSpeech);
                var form = lexeme.GetOrCreateForm(lexeme, normalizedWord, inflection);
                form.References.Add(reference);
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
