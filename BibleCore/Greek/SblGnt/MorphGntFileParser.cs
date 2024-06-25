using BibleCore.Properties;

using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Core;

namespace BibleCore.Greek.SblGnt
{
    public class MorphGntFileParser
    {
        public static void Parse(Text text, Lexicon lexicon)
        {
            int lineCount = 0;

            ReadBook(text, lexicon, "_61_Mt_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_62_Mk_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_63_Lk_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_64_Jn_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_65_Ac_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_66_Ro_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_67_1Co_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_68_2Co_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_69_Ga_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_70_Eph_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_71_Php_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_72_Col_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_73_1Th_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_74_2Th_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_75_1Ti_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_76_2Ti_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_77_Tit_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_78_Phm_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_79_Heb_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_80_Jas_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_81_1Pe_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_82_2Pe_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_83_1Jn_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_84_2Jn_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_85_3Jn_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_86_Jud_morphgnt", ref lineCount);
            ReadBook(text, lexicon, "_87_Re_morphgnt", ref lineCount);

            Console.WriteLine(lineCount + "lines processed.");
        }

        private static void ReadBook(Text text, Lexicon lexicon, string fileName, ref int lineCount)
        {
            var bookText = Resources.ResourceManager.GetString(fileName);
            ArgumentNullException.ThrowIfNull(bookText, nameof(bookText));

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var reader = new StreamReader(stream);

            //var inflections = new HashSet<string>();

            string? line;

            string currentBookChapterVerse = "";
            Books currentBook = default;
            byte currentChapter = default;
            byte currentVerse = default;
            byte currentPosition = default;

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
                string textValue = fields[3];
                string word = fields[4];
                string normalizedWord = fields[5];
                string lemma = fields[6];

                if (bookChapterVerse != currentBookChapterVerse)
                {
                    currentBookChapterVerse = bookChapterVerse;
                    currentBook = ParseBook(bookChapterVerse[..2]);
                    currentChapter = byte.Parse(bookChapterVerse[2..4]);
                    currentVerse = byte.Parse(bookChapterVerse[4..]);
                    currentPosition = 0;
                }
                else
                {
                    ++currentPosition;
                }

                var textEntryBookmark = new TextEntryBookmark()
                {
                    Book = currentBook,
                    Chapter = currentChapter,
                    Verse = currentVerse,
                    Position = currentPosition
                };

                var partOfSpeech = ParsePartOfSpeech(partOfSpeechCode);
                var inflection = new InflectionBuilder().ParseInflection(parsingCode).Build();
                var reference = new Reference() { Bookmark = textEntryBookmark };

                var lexeme = lexicon.GetOrCreateLexeme(lemma, partOfSpeech);
                var form = lexeme.GetOrCreateForm(lexeme, normalizedWord, inflection);
                form.References.Add(reference);

                var textEntry = text.CreateTextEntry(textEntryBookmark, textValue, word, normalizedWord);

            }
        }

        private static Books ParseBook(string value)
        {
            return value switch
            {
                "01" => Books.Matthew,
                "02" => Books.Mark,
                "03" => Books.Luke,
                "04" => Books.John,
                "05" => Books.Acts,
                "06" => Books.Romans,
                "07" => Books.FirstCorinthians,
                "08" => Books.SecondCorinthians,
                "09" => Books.Galatians,
                "10" => Books.Ephesians,
                "11" => Books.Philippians,
                "12" => Books.Colossians,
                "13" => Books.FirstThessalonians,
                "14" => Books.SecondThessalonians,
                "15" => Books.FirstTimothy,
                "16" => Books.SecondTimothy,
                "17" => Books.Titus,
                "18" => Books.Philemon,
                "19" => Books.Hebrews,
                "20" => Books.James,
                "21" => Books.FirstPeter,
                "22" => Books.SecondPeter,
                "23" => Books.FirstJohn,
                "24" => Books.SecondJohn,
                "25" => Books.ThirdJohn,
                "26" => Books.Jude,
                "27" => Books.Revelation,
                _ => throw new NotImplementedException()
            };
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
