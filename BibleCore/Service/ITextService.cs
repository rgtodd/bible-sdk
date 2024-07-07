using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface ITextService
    {
        TextData? GetText(string? rangeExpression);

        TextData? MovePreviousText(string? rangeExpression);

        TextData? MoveNextText(string? rangeExpression);

        TextData? ExtendPreviousText(string? rangeExpression);

        TextData? ExtendNextText(string? rangeExpression);
    }
}