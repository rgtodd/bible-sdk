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
        public PersonData? Person { get; init; }

        public TenseData? Tense { get; init; }

        public VoiceData? Voice { get; init; }

        public MoodData? Mood { get; init; }

        public CaseData? Case { get; init; }

        public NumberData? Number { get; init; }

        public GenderData? Gender { get; init; }

        public DegreeData? Degree { get; init; }

    }
}
