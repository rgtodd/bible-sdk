using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public readonly struct Inflection : IEquatable<Inflection>
    {
        public Persons? Person { get; init; }

        public Tenses? Tense { get; init; }

        public Voices? Voice { get; init; }

        public Moods? Mood { get; init; }

        public Cases? Case { get; init; }

        public Numbers? Number { get; init; }

        public Genders? Gender { get; init; }

        public Degrees? Degree { get; init; }

        public static string ToCode(Persons? persons)
        {
            return persons switch
            {
                null => "---",
                Persons.First => "1ST",
                Persons.Second => "2ND",
                Persons.Third => "3RD",
                _ => "???"
            };
        }

        public static string ToCode(Tenses? tense)
        {
            return tense switch
            {
                null => "---",
                Tenses.Present => "PRE",
                Tenses.Imperfect => "IMP",
                Tenses.Future => "FUT",
                Tenses.Aorist => "AOR",
                Tenses.Perfect => "PER",
                Tenses.Pluperfect => "PLU",
                _ => "???"
            };
        }

        public static string ToCode(Voices? voice)
        {
            return voice switch
            {
                null => "---",
                Voices.Active => "ACT",
                Voices.Middle => "MID",
                Voices.Passive => "PAS",
                _ => "???"
            };
        }

        public static string ToCode(Moods? mood)
        {
            return mood switch
            {
                null => "---",
                Moods.Indicative => "IND",
                Moods.Imperative => "IMP",
                Moods.Subjunctive => "SUB",
                Moods.Optative => "OPT",
                Moods.Infinitive => "INF",
                Moods.Participle => "PAR",
                _ => "???"
            };
        }

        public static string ToCode(Cases? _case)
        {
            return _case switch
            {
                null => "---",
                Cases.Nominative => "NOM",
                Cases.Genitive => "GEN",
                Cases.Dative => "DAT",
                Cases.Accusative => "ACC",
                _ => "???"
            };
        }

        public static string ToCode(Numbers? number)
        {
            return number switch
            {
                null => "---",
                Numbers.Singular => "SIN",
                Numbers.Plural => "PLU",
                _ => "???"
            };
        }

        public static string ToCode(Genders? gender)
        {
            return gender switch
            {
                null => "---",
                Genders.Masculine => "MAS",
                Genders.Feminine => "FEM",
                Genders.Neuter => "NEU",
                _ => "???"
            };
        }

        public static string ToCode(Degrees? degree)
        {
            return degree switch
            {
                null => "---",
                Degrees.Comparative => "COM",
                Degrees.Superlative => "SUP",
                _ => "???"
            };
        }

        public override string ToString()
        {
            return ToCode(Person) + "-" + ToCode(Tense) + "-" + ToCode(Voice) + "-" + ToCode(Mood) + "-" + ToCode(Case) + "-" + ToCode(Number) + "-" + ToCode(Gender) + "-" + ToCode(Degree);
        }

        public override bool Equals(object? obj)
        {
            return obj is Inflection inflection && Equals(inflection);
        }

        public bool Equals(Inflection other)
        {
            return Person == other.Person &&
                   Tense == other.Tense &&
                   Voice == other.Voice &&
                   Mood == other.Mood &&
                   Case == other.Case &&
                   Number == other.Number &&
                   Gender == other.Gender &&
                   Degree == other.Degree;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Person, Tense, Voice, Mood, Case, Number, Gender, Degree);
        }

        public static bool operator ==(Inflection left, Inflection right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Inflection left, Inflection right)
        {
            return !(left == right);
        }
    }
}
