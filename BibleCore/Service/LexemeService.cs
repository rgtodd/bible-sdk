using BibleCore.Greek;
using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class LexemeService(IGlobalGreek globalGreek) : ILexemeService
    {
        public LexemeData? GetByStrongsNumber(int strongsNumber)
        {
            var lexeme = globalGreek.Lexicon.GetByStrongsNumber(strongsNumber);
            if (lexeme == null)
            {
                return null;
            }

            var lexemeData = DataFactory.CreateLexemeData(lexeme);
            return lexemeData;
        }

        public LexemeData? GetByGkNumber(int gkNumber)
        {
            var lexeme = globalGreek.Lexicon.GetByGkNumber(gkNumber);
            if (lexeme == null)
            {
                return null;
            }

            var lexemeData = DataFactory.CreateLexemeData(lexeme);
            return lexemeData;
        }

        public List<LexemeData> GetLexemes(int? minimumMounceChapter, int? maximumMounceChapter, string? rangeExpression)
        {
            List<LexemeData> result = [];

            List<Lexeme> lexemes;

            if (rangeExpression != null)
            {
                var range = Greek.Range.Parse(rangeExpression);

                var textEntries = globalGreek.Text.Select(range, 10000);
                lexemes = [.. textEntries.Select(te => te.Lexeme).Distinct()];
            }
            else
            {
                lexemes = globalGreek.Lexicon.Lexemes;
            }

            foreach (var lexeme in lexemes)
            {
                bool includeLexeme = true; // assume success

                if (minimumMounceChapter != null)
                {
                    if (lexeme.MounceChapterNumber == 0 || lexeme.MounceChapterNumber < minimumMounceChapter)
                    {
                        includeLexeme = false;
                    }
                }

                if (maximumMounceChapter != null)
                {
                    if (lexeme.MounceChapterNumber == 0 || lexeme.MounceChapterNumber > maximumMounceChapter)
                    {
                        includeLexeme = false;
                    }
                }

                if (includeLexeme)
                {
                    result.Add(DataFactory.CreateLexemeData(lexeme));
                }
            }

            return result;
        }
    }
}
