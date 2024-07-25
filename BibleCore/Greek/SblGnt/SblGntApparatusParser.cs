using BibleCore.Properties;

using Microsoft.Extensions.Logging;

using System.Text;
using System.Xml;

namespace BibleCore.Greek.SblGnt
{
    internal class SblGntApparatusParser
    {
        public static void Parse(ILogger logger, Apparatus apparatus)
        {
            int lineCount = 0;

            ReadBook(logger, apparatus, "_1Cor", ref lineCount);
            ReadBook(logger, apparatus, "_1John", ref lineCount);
            ReadBook(logger, apparatus, "_1Pet", ref lineCount);
            ReadBook(logger, apparatus, "_1Thess", ref lineCount);
            ReadBook(logger, apparatus, "_1Tim", ref lineCount);
            ReadBook(logger, apparatus, "_2Cor", ref lineCount);
            ReadBook(logger, apparatus, "_2John", ref lineCount);
            ReadBook(logger, apparatus, "_2Pet", ref lineCount);
            ReadBook(logger, apparatus, "_2Thess", ref lineCount);
            ReadBook(logger, apparatus, "_2Tim", ref lineCount);
            ReadBook(logger, apparatus, "_3John", ref lineCount);
            ReadBook(logger, apparatus, "Acts", ref lineCount);
            ReadBook(logger, apparatus, "Col", ref lineCount);
            ReadBook(logger, apparatus, "Eph", ref lineCount);
            ReadBook(logger, apparatus, "Gal", ref lineCount);
            ReadBook(logger, apparatus, "Heb", ref lineCount);
            ReadBook(logger, apparatus, "Jas", ref lineCount);
            ReadBook(logger, apparatus, "John", ref lineCount);
            ReadBook(logger, apparatus, "Jude", ref lineCount);
            ReadBook(logger, apparatus, "Luke", ref lineCount);
            ReadBook(logger, apparatus, "Mark", ref lineCount);
            ReadBook(logger, apparatus, "Matt", ref lineCount);
            ReadBook(logger, apparatus, "Phil", ref lineCount);
            ReadBook(logger, apparatus, "Phlm", ref lineCount);
            ReadBook(logger, apparatus, "Rev", ref lineCount);
            ReadBook(logger, apparatus, "Rom", ref lineCount);
            ReadBook(logger, apparatus, "Titus", ref lineCount);

            logger.LogInformation("{lineCount} lines processed.", lineCount);
        }

        private static void ReadBook(ILogger logger, Apparatus apparatus, string fileName, ref int lineCount)
        {
            var bookText = Resources.ResourceManager.GetString(fileName);
            ArgumentNullException.ThrowIfNull(bookText, nameof(bookText));

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(bookText));
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            var settings = new XmlReaderSettings();

            using var reader = XmlReader.Create(stream, settings);

            Bookmark? bookmark = null;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "verse")
                {
                    var verse = reader.ReadElementContentAsString();
                    logger.LogDebug("Note {verse}", verse);

                    bookmark = Bookmark.Parse(verse);
                }
                else if (reader.NodeType == XmlNodeType.Element && reader.Name == "note")
                {
                    ArgumentNullException.ThrowIfNull(bookmark, nameof(bookmark));

                    var note = reader.ReadElementContentAsString();
                    apparatus.CreateApparatusEntry(bookmark.Value, note);
                }
            }
        }
    }
}
