using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public class LexemeListModel
    {
        public required List<LexemeData> Lexemes { get; init; }
    }
}
