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
    }
}
