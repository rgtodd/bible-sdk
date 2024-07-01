using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface ILexemeService
    {
        LexemeData? GetByStrongsNumber(int strongsNumber);
    }
}