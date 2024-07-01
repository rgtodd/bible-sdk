using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class ExerciseWordData
    {
        public required string Lemma { get; init; }

        public required int Strongs { get; init; }

        public int DefinitionMastery { get; init; } = 0;

        public int PartOfSpeechMastery { get; init; } = 0;

        public required string Gloss { get; init; }

        public required string[] Glosses { get; init; }

        public required PartOfSpeechData PartOfSpeech { get; init; }

        public required PartOfSpeechData[] PartsOfSpeech { get; init; }
    }
}
