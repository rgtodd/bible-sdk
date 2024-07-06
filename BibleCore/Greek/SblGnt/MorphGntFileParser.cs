using BibleCore.Properties;

using Microsoft.Extensions.Logging;

using System.Text;

namespace BibleCore.Greek.SblGnt
{
    internal class MorphGntFileParser
    {
        public static void Parse(ILogger logger, Text text, Lexicon lexicon)
        {
            int lineCount = 0;

            ReadBook(logger, text, lexicon, "_61_Mt_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_62_Mk_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_63_Lk_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_64_Jn_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_65_Ac_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_66_Ro_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_67_1Co_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_68_2Co_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_69_Ga_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_70_Eph_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_71_Php_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_72_Col_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_73_1Th_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_74_2Th_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_75_1Ti_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_76_2Ti_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_77_Tit_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_78_Phm_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_79_Heb_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_80_Jas_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_81_1Pe_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_82_2Pe_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_83_1Jn_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_84_2Jn_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_85_3Jn_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_86_Jud_morphgnt", ref lineCount);
            ReadBook(logger, text, lexicon, "_87_Re_morphgnt", ref lineCount);

            logger.LogInformation("{lineCount} lines processed.", lineCount);
        }

        private static void ReadBook(ILogger logger, Text text, Lexicon lexicon, string fileName, ref int lineCount)
        {
            var bookText = Resources.ResourceManager.GetString(fileName);
            ArgumentNullException.ThrowIfNull(bookText, nameof(bookText));

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var reader = new StreamReader(stream);

            //var inflections = new HashSet<string>();

            string? line;

            string currentBookChapterVerse = "";
            Book currentBook = default;
            byte currentChapter = default;
            byte currentVerse = default;
            byte currentPosition = default;

            while ((line = reader.ReadLine()) != null)
            {
                ++lineCount;
                string[] fields = line.Split(' ');
                if (fields.Length != 7)
                {
                    logger.LogError("Line {line} has unexpected field count of {count}.", line, fields.Length);
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

                var bookmark = new Bookmark()
                {
                    Book = currentBook,
                    Chapter = currentChapter,
                    Verse = currentVerse,
                    Position = currentPosition
                };

                var partOfSpeech = ParsePartOfSpeech(partOfSpeechCode);
                var inflection = new InflectionBuilder().ParseInflection(parsingCode).Build();

                var lexeme = lexicon.GetOrCreateLexeme(lemma, partOfSpeech);
                var form = lexeme.GetOrCreateForm(lexeme, normalizedWord, inflection);
                form.Bookmarks.Add(bookmark);

                var textEntry = text.CreateTextEntry(bookmark, textValue, word, normalizedWord, lexeme);

            }
        }

        private static Book ParseBook(string value)
        {
            return value switch
            {
                "01" => Book.Matthew,
                "02" => Book.Mark,
                "03" => Book.Luke,
                "04" => Book.John,
                "05" => Book.Acts,
                "06" => Book.Romans,
                "07" => Book.FirstCorinthians,
                "08" => Book.SecondCorinthians,
                "09" => Book.Galatians,
                "10" => Book.Ephesians,
                "11" => Book.Philippians,
                "12" => Book.Colossians,
                "13" => Book.FirstThessalonians,
                "14" => Book.SecondThessalonians,
                "15" => Book.FirstTimothy,
                "16" => Book.SecondTimothy,
                "17" => Book.Titus,
                "18" => Book.Philemon,
                "19" => Book.Hebrews,
                "20" => Book.James,
                "21" => Book.FirstPeter,
                "22" => Book.SecondPeter,
                "23" => Book.FirstJohn,
                "24" => Book.SecondJohn,
                "25" => Book.ThirdJohn,
                "26" => Book.Jude,
                "27" => Book.Revelation,
                _ => throw new NotImplementedException()
            };
        }

        private static PartOfSpeech ParsePartOfSpeech(string value)
        {
            return value switch
            {
                "A-" => PartOfSpeech.Adjective,
                "C-" => PartOfSpeech.Conjunction,
                "D-" => PartOfSpeech.Adverb,
                "I-" => PartOfSpeech.Interjection,
                "N-" => PartOfSpeech.Noun,
                "P-" => PartOfSpeech.Preposition,
                "RA" => PartOfSpeech.DefiniteArticle,
                "RD" => PartOfSpeech.DemonstrativePronoun,
                "RI" => PartOfSpeech.IndefinitePronoun,
                "RP" => PartOfSpeech.PersonalPronoun,
                "RR" => PartOfSpeech.RelativePronoun,
                "V-" => PartOfSpeech.Verb,
                "X-" => PartOfSpeech.Particle,
                _ => throw new NotImplementedException()
            };
        }
    }
}
