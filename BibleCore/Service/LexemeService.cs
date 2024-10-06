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

        public List<LexemeData> GetLexemes(int? minimumMounceChapter, int? maximumMounceChapter)
        {
            List<LexemeData> result = [];

            foreach (var lexeme in globalGreek.Lexicon.Lexemes)
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
