namespace BibleCore.Greek
{
    internal class WordList(Lexeme[] lexemes, string description, int mounceChapterNumber)
    {
        public Lexeme[] Lexemes => lexemes;

        public string Description => description;

        public int MounceChapterNumber => mounceChapterNumber;

        public static WordList CreateForMounceChapter(Lexicon lexicon, int mounceChapterNumber)
        {
            var lexemes = lexicon.GetByMounceChapter(mounceChapterNumber).ToArray();
            return new WordList(lexemes, $"Mounce Chapter {mounceChapterNumber}", mounceChapterNumber);
        }

        public static WordList CreateForText(Text text, Range range, int maxEntries)
        {
            var textEntries = text.Select(range, maxEntries);
            var lexemes = textEntries.Select(te => te.Lexeme).Distinct().ToArray();
            return new WordList(lexemes, range.Format(), 0);
        }
    }
}
