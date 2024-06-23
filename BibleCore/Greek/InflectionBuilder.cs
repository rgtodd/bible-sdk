using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class InflectionBuilder
    {
        private Persons? m_person;
        private Tenses? m_tense;
        private Voices? m_voice;
        private Moods? m_mood;
        private Cases? m_case;
        private Numbers? m_number;
        private Genders? m_gender;
        private Degrees? m_degree;

        public InflectionBuilder SetPerson(Persons? person)
        {
            m_person = person;
            return this;
        }

        public InflectionBuilder SetTense(Tenses? tense)
        {
            m_tense = tense;
            return this;
        }

        public InflectionBuilder SetVoice(Voices? voice)
        {
            m_voice = voice;
            return this;
        }

        public InflectionBuilder SetMood(Moods? mood)
        {
            m_mood = mood;
            return this;
        }

        public InflectionBuilder SetCase(Cases? _case)
        {
            m_case = _case;
            return this;
        }

        public InflectionBuilder SetNumber(Numbers? number)
        {
            m_number = number;
            return this;
        }

        public InflectionBuilder SetGender(Genders? gender)
        {
            m_gender = gender;
            return this;
        }

        public InflectionBuilder SetDegree(Degrees? degree)
        {
            m_degree = degree;
            return this;
        }

        public InflectionBuilder ParseInflection(string inflection)
        {
            ArgumentNullException.ThrowIfNull(inflection, nameof(inflection));
            if (inflection.Length != 8) throw new ArgumentOutOfRangeException(nameof(inflection));

            if (inflection[0] != '-')
            {
                SetPerson(ParsePerson(inflection[0]));
            }
            if (inflection[1] != '-')
            {
                SetTense(ParseTense(inflection[1]));
            }
            if (inflection[2] != '-')
            {
                SetVoice(ParseVoice(inflection[2]));
            }
            if (inflection[3] != '-')
            {
                SetMood(ParseMood(inflection[3]));
            }
            if (inflection[4] != '-')
            {
                SetCase(ParseCase(inflection[4]));
            }
            if (inflection[5] != '-')
            {
                SetNumber(ParseNumber(inflection[5]));
            }
            if (inflection[6] != '-')
            {
                SetGender(ParseGender(inflection[6]));
            }
            if (inflection[7] != '-')
            {
                SetDegree(ParseDegree(inflection[7]));
            }

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

        public static Persons ParsePerson(char value)
        {
            switch (value)
            {
                case '1': return Persons.First;
                case '2': return Persons.Second;
                case '3': return Persons.Third;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Tenses ParseTense(char value)
        {
            switch (value)
            {
                case 'P': return Tenses.Present;
                case 'I': return Tenses.Imperfect;
                case 'F': return Tenses.Future;
                case 'A': return Tenses.Aorist;
                case 'X': return Tenses.Perfect;
                case 'Y': return Tenses.Pluperfect;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Voices ParseVoice(char value)
        {
            switch (value)
            {
                case 'A': return Voices.Active;
                case 'M': return Voices.Middle;
                case 'P': return Voices.Passive;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Moods ParseMood(char value)
        {
            switch (value)
            {
                case 'I': return Moods.Indicative;
                case 'D': return Moods.Imperative;
                case 'S': return Moods.Subjunctive;
                case 'O': return Moods.Optative;
                case 'N': return Moods.Infinitive;
                case 'P': return Moods.Participle;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Cases ParseCase(char value)
        {
            switch (value)
            {
                case 'N': return Cases.Nominative;
                case 'G': return Cases.Genitive;
                case 'D': return Cases.Dative;
                case 'A': return Cases.Accusative;
                case 'V': return Cases.V;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Numbers ParseNumber(char value)
        {
            switch (value)
            {
                case 'S': return Numbers.Singular;
                case 'P': return Numbers.Plural;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Genders ParseGender(char value)
        {
            switch (value)
            {
                case 'M': return Genders.Masculine;
                case 'F': return Genders.Feminine;
                case 'N': return Genders.Neuter;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Degrees ParseDegree(char value)
        {
            switch (value)
            {
                case 'C': return Degrees.Comparative;
                case 'S': return Degrees.Superlative;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }
    }
}
