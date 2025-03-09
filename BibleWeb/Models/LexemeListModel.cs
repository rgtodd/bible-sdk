using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public class LexemeListModel
    {
        public required IList<LexemeCatagoryModel> Categories { get; init; }
    }

    public class LexemeCatagoryModel
    {
        public required PartOfSpeechData PartOfSpeech { get; init; }

        public required IList<LexemeData> Lexemes { get; init; }
    }
}
