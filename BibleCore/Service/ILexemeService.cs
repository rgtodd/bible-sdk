using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface ILexemeService
    {
        LexemeData? GetByStrongsNumber(int strongsNumber);

        LexemeData? GetByGkNumber(int gkNumber);

        List<LexemeData> GetLexemes(int? minimumMounceChapter, int? maximumMounceChapter);
    }
}