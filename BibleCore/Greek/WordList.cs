namespace BibleCore.Greek
{
    internal class WordList(Lexeme[] lexemes, string description, string? wordListId, string? range)
    {
        public Lexeme[] Lexemes => lexemes;

        public string Description => description;

        public string? WordListId => wordListId;

        public string? Range => range;

        public static WordList CreateForMounceChapter(Lexicon lexicon, int mounceChapterNumber)
        {
            var lexemes = lexicon.GetByMounceChapter(mounceChapterNumber).ToArray();
            return new WordList(lexemes, $"Mounce Chapter {mounceChapterNumber}", $"m-{mounceChapterNumber}", null);
        }

        public static WordList CreateForText(Text text, Range range, int maxEntries)
        {
            var textEntries = text.Select(range, maxEntries);
            var lexemes = textEntries.Select(te => te.Lexeme).Distinct().ToArray();
            return new WordList(lexemes, range.Format(), null, range.Format());
        }
    }
}
