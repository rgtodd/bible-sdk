using BibleCore.Properties;

using System.Diagnostics;
using System.Text;

namespace BibleCore.Greek.SblGnt
{
    internal static class MounceParser
    {
        public static void Parse(Lexicon lexicon)
        {
            var bookText = Resources.mounce;

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var reader = new StreamReader(stream);

            var line = reader.ReadLine();
            while (line != null)
            {
                var fields = line.Split('\t');

                int gkNumber = int.Parse(fields[0]);
                int chapter = int.Parse(fields[1]);

                var lexeme = lexicon.GetByGkNumber(gkNumber);
                if (lexeme == null)
                {
                    Debug.WriteLine($"Can't find GK number {gkNumber}.");
                }
                else
                {
                    lexeme.MounceChapterNumber = chapter;
                    lexicon.MounceChapterWordCount[chapter] = lexicon.MounceChapterWordCount.TryGetValue(chapter, out int count) ? count + 1 : 1;
                }

                line = reader.ReadLine();
            }
        }
    }
}