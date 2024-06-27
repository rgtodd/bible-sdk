using BibleCore.Greek;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class InflectionData
    {
        public required PersonData? Person { get; init; }

        public required TenseData? Tense { get; init; }

        public required VoiceData? Voice { get; init; }

        public required MoodData? Mood { get; init; }

        public required CaseData? Case { get; init; }

        public required NumberData? Number { get; init; }

        public required GenderData? Gender { get; init; }

        public required DegreeData? Degree { get; init; }

    }
}
