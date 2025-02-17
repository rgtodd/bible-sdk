namespace BibleCore.Greek
{
    internal readonly struct Inflection : IEquatable<Inflection>, IComparable<Inflection>
    {
        public Mood? Mood { get; init; }

        public Tense? Tense { get; init; }

        public Voice? Voice { get; init; }

        public Case? Case { get; init; }

        public Person? Person { get; init; }

        public Number? Number { get; init; }

        public Gender? Gender { get; init; }

        public Degree? Degree { get; init; }

        private static string ToCode(Person? persons)
        {
            return persons switch
            {
                null => "---",
                Greek.Person.First => "1ST",
                Greek.Person.Second => "2ND",
                Greek.Person.Third => "3RD",
                _ => "???"
            };
        }

        private static string ToCode(Tense? tense)
        {
            return tense switch
            {
                null => "---",
                Greek.Tense.Present => "PRE",
                Greek.Tense.Imperfect => "IMP",
                Greek.Tense.Future => "FUT",
                Greek.Tense.Aorist => "AOR",
                Greek.Tense.Perfect => "PER",
                Greek.Tense.Pluperfect => "PLU",
                _ => "???"
            };
        }

        private static string ToCode(Voice? voice)
        {
            return voice switch
            {
                null => "---",
                Greek.Voice.Active => "ACT",
                Greek.Voice.Middle => "MID",
                Greek.Voice.Passive => "PAS",
                _ => "???"
            };
        }

        private static string ToCode(Mood? mood)
        {
            return mood switch
            {
                null => "---",
                Greek.Mood.Indicative => "IND",
                Greek.Mood.Imperative => "IMP",
                Greek.Mood.Subjunctive => "SUB",
                Greek.Mood.Optative => "OPT",
                Greek.Mood.Infinitive => "INF",
                Greek.Mood.Participle => "PAR",
                _ => "???"
            };
        }

        private static string ToCode(Case? _case)
        {
            return _case switch
            {
                null => "---",
                Greek.Case.Nominative => "NOM",
                Greek.Case.Genitive => "GEN",
                Greek.Case.Dative => "DAT",
                Greek.Case.Accusative => "ACC",
                _ => "???"
            };
        }

        private static string ToCode(Number? number)
        {
            return number switch
            {
                null => "---",
                Greek.Number.Singular => "SIN",
                Greek.Number.Plural => "PLU",
                _ => "???"
            };
        }

        private static string ToCode(Gender? gender)
        {
            return gender switch
            {
                null => "---",
                Greek.Gender.Masculine => "MAS",
                Greek.Gender.Feminine => "FEM",
                Greek.Gender.Neuter => "NEU",
                _ => "???"
            };
        }

        private static string ToCode(Degree? degree)
        {
            return degree switch
            {
                null => "---",
                Greek.Degree.Comparative => "COM",
                Greek.Degree.Superlative => "SUP",
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

        public int CompareTo(Inflection other)
        {
            int result = 0;
            if (result == 0)
            {
                result = ((int?)Mood ?? 0).CompareTo((int?)other.Mood ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Tense ?? 0).CompareTo((int?)other.Tense ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Voice ?? 0).CompareTo((int?)other.Voice ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Case ?? 0).CompareTo((int?)other.Case ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Person ?? 0).CompareTo((int?)other.Person ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Number ?? 0).CompareTo((int?)other.Number ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Gender ?? 0).CompareTo((int?)other.Gender ?? 0);
            }
            if (result == 0)
            {
                result = ((int?)Degree ?? int.MinValue).CompareTo((int?)other.Degree ?? int.MaxValue);
            }

            return result;
        }

        public static bool operator ==(Inflection left, Inflection right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Inflection left, Inflection right)
        {
            return !(left == right);
        }

        public static bool operator <(Inflection left, Inflection right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Inflection left, Inflection right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Inflection left, Inflection right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Inflection left, Inflection right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
