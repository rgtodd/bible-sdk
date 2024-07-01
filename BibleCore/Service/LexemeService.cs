using BibleCore.Service.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service
{
    public class LexemeService(IGlobalGreek globalGreek) : ILexemeService
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
