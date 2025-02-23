namespace BibleCore.Greek
{
    internal class InflectionBuilder
    {
        private Person m_person;
        private Tense m_tense;
        private Voice m_voice;
        private Mood m_mood;
        private Case m_case;
        private Number m_number;
        private Gender m_gender;
        private Degree m_degree;

        public InflectionBuilder SetPerson(Person person)
        {
            m_person = person;
            return this;
        }

        public InflectionBuilder SetTense(Tense tense)
        {
            m_tense = tense;
            return this;
        }

        public InflectionBuilder SetVoice(Voice voice)
        {
            m_voice = voice;
            return this;
        }

        public InflectionBuilder SetMood(Mood mood)
        {
            m_mood = mood;
            return this;
        }

        public InflectionBuilder SetCase(Case _case)
        {
            m_case = _case;
            return this;
        }

        public InflectionBuilder SetNumber(Number number)
        {
            m_number = number;
            return this;
        }

        public InflectionBuilder SetGender(Gender gender)
        {
            m_gender = gender;
            return this;
        }

        public InflectionBuilder SetDegree(Degree degree)
        {
            m_degree = degree;
            return this;
        }

        public InflectionBuilder ParseInflection(string inflection)
        {
            ArgumentNullException.ThrowIfNull(inflection, nameof(inflection));
            if (inflection.Length != 8) throw new ArgumentOutOfRangeException(nameof(inflection));

            SetPerson(ParsePerson(inflection[0]));
            SetTense(ParseTense(inflection[1]));
            SetVoice(ParseVoice(inflection[2]));
            SetMood(ParseMood(inflection[3]));
            SetCase(ParseCase(inflection[4]));
            SetNumber(ParseNumber(inflection[5]));
            SetGender(ParseGender(inflection[6]));
            SetDegree(ParseDegree(inflection[7]));

            return this;
        }

        public Inflection Build()
        {
            return new Inflection()
            {
                Person = m_person,
                Tense = m_tense,
                Voice = m_voice,
                Mood = m_mood,
                Case = m_case,
                Number = m_number,
                Gender = m_gender,
                Degree = m_degree
            };
        }

        private static Person ParsePerson(char value)
        {
            return value switch
            {
                '1' => Person.First,
                '2' => Person.Second,
                '3' => Person.Third,
                '-' => Person.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Tense ParseTense(char value)
        {
            return value switch
            {
                'P' => Tense.Present,
                'I' => Tense.Imperfect,
                'F' => Tense.Future,
                'A' => Tense.Aorist,
                'X' => Tense.Perfect,
                'Y' => Tense.Pluperfect,
                '-' => Tense.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Voice ParseVoice(char value)
        {
            return value switch
            {
                'A' => Voice.Active,
                'M' => Voice.Middle,
                'P' => Voice.Passive,
                '-' => Voice.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Mood ParseMood(char value)
        {
            return value switch
            {
                'I' => Mood.Indicative,
                'D' => Mood.Imperative,
                'S' => Mood.Subjunctive,
                'O' => Mood.Optative,
                'N' => Mood.Infinitive,
                'P' => Mood.Participle,
                '-' => Mood.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Case ParseCase(char value)
        {
            return value switch
            {
                'N' => Case.Nominative,
                'G' => Case.Genitive,
                'D' => Case.Dative,
                'A' => Case.Accusative,
                'V' => Case.Vocative,
                '-' => Case.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Number ParseNumber(char value)
        {
            return value switch
            {
                'S' => Number.Singular,
                'P' => Number.Plural,
                '-' => Number.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Gender ParseGender(char value)
        {
            return value switch
            {
                'M' => Gender.Masculine,
                'F' => Gender.Feminine,
                'N' => Gender.Neuter,
                '-' => Gender.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }

        private static Degree ParseDegree(char value)
        {
            return value switch
            {
                'C' => Degree.Comparative,
                'S' => Degree.Superlative,
                '-' => Degree.Unknown,
                _ => throw new ArgumentOutOfRangeException(paramName: nameof(value)),
            };
        }
    }
}
