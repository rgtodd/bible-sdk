using BibleCore.Service.Data;

namespace BibleCore.Service
{
    internal class LexemeService(IGlobalGreek globalGreek) : ILexemeService
    {
        public LexemeData? GetByStrongsNumber(int strongsNumber)
        {
            var lexeme = globalGreek.Lexicon.GetByStrongs(strongsNumber);
            if (lexeme == null)
            {
                return null;
            }

            var lexemeData = DataFactory.CreateLexemeData(lexeme);
            return lexemeData;
        }
    }
}
