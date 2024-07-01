using BibleCore.Properties;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.SblGnt
{
    public static class FlashworksGreekParser
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
                int gkNumber = int.Parse(fields[5]);

                var lexeme = lexicon.GetByGkNumber(gkNumber);
                if (lexeme == null)
                {
                    Debug.WriteLine($"Can't find GK number {gkNumber}.");
                }
                else
                {
                    lexeme.MounceChapterNumber = chapter;
                }

                line = reader.ReadLine();
            }
        }
    }
}