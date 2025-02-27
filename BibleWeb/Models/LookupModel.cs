using BibleCore.Service.Data;

namespace BibleWebApi.Models
{
    public class LookupModel
    {
        public required string? Message { get; init; }

        public required LexemeData? LexemeData { get; init; }

        public required int? StrongsNumber { get; init; }

        public required int? GkNumber { get; init; }

        public required string? Range { get; init; }

        public required VerbModel? Verb { get; init; }
    }

}
