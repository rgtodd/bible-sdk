using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface ITextService
    {
        TextData? GetText(string? rangeExpression);
    }
}