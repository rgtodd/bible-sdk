using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class LexemeData
    {
        public static LexemeData Empty { get; } = new()
        {
            Lemma = string.Empty,
            PartOfSpeech = PartOfSpeechData.Noun,
            Gloss = string.Empty,
            Strongs = [],
            Gk = [],
            Forms = []
        };

        public required string Lemma { get; init; }

        public required PartOfSpeechData PartOfSpeech { get; init; }

        public required string Gloss { get; init; }

        public required int[] Strongs { get; init; }

        public required int[] Gk { get; init; }

        public required FormData[] Forms { get; init; }
    }
}
