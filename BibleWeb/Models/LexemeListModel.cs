using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public class LexemeListModel
    {
        public required List<LexemeData> Lexemes { get; init; }
    }
}
