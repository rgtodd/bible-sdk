using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public class LookupModel
    {
        public string? Message { get; set; }

        public LexemeData? LexemeData { get; set; }

        public int? StrongsNumber { get; set; }

        public int? GkNumber { get; set; }
    }
}
