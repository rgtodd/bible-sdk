using BibleCore.Properties;

using Microsoft.Extensions.Logging;

using System.Text;

namespace BibleCore.Greek.SblGnt
{
    // Parses the mounce.txt file. This is a tab-delimited file. Each record
    // contains a GK number and the Mounce chapter number in which the word
    // is introduced.
    //
    // Using this information, the entries in the lexicon are enhanced
    // with the following information:
    //
    // Lexicon:
    // * MounceChapterWordCount
    // * Lexeme:
    //   * MounceChapterNumber
    //
    internal static class MounceParser
    {
        public static void Parse(ILogger logger, Lexicon lexicon)
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
                    logger.LogInformation("Can't find GK number {gkNumber} for chapter {chapter}.", gkNumber, chapter);
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