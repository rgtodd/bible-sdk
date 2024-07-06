using BibleCore.Properties;

using System.Diagnostics;
using System.Text;

namespace BibleCore.Greek.SblGnt
{
    internal static class FlashworksGreekParser
    {
        public static void Parse(Lexicon lexicon)
        {
            var bookText = Resources.flashworksgreek;

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var reader = new StreamReader(stream);

            var line = reader.ReadLine();
            while (line != null)
            {
                var fields = line.Split('\t');

                int chapter = int.Parse(fields[0]);
                string lemma = fields[3];
                string entry = fields[4];
                int gkNumber = int.Parse(fields[5]);

                var lexeme = lexicon.GetByGkNumber(gkNumber);
                if (lexeme == null)
                {
                    Debug.WriteLine($"Can't find GK number {gkNumber} for {lemma}.");
                }
                else
                {
                    lexeme.MounceChapterNumber = chapter;

                    if (lexeme.Gloss == string.Empty)
                    {
                        Debug.WriteLine($"Gloss updated for {lemma}");
                        lexeme.Gloss = entry;
                    }

                    lexicon.MounceChapterWordCount[chapter] = lexicon.MounceChapterWordCount.TryGetValue(chapter, out int count) ? count + 1 : 1;
                }

                line = reader.ReadLine();
            }
        }
    }
}