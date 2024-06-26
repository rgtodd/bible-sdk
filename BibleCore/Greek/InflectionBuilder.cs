using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class InflectionBuilder
    {
        private Person? m_person;
        private Tense? m_tense;
        private Voice? m_voice;
        private Mood? m_mood;
        private Case? m_case;
        private Number? m_number;
        private Gender? m_gender;
        private Degree? m_degree;

        public InflectionBuilder SetPerson(Person? person)
        {
            m_person = person;
            return this;
        }

        public InflectionBuilder SetTense(Tense? tense)
        {
            m_tense = tense;
            return this;
        }

        public InflectionBuilder SetVoice(Voice? voice)
        {
            m_voice = voice;
            return this;
        }

        public InflectionBuilder SetMood(Mood? mood)
        {
            m_mood = mood;
            return this;
        }

        public InflectionBuilder SetCase(Case? _case)
        {
            m_case = _case;
            return this;
        }

        public InflectionBuilder SetNumber(Number? number)
        {
            m_number = number;
            return this;
        }

        public InflectionBuilder SetGender(Gender? gender)
        {
            m_gender = gender;
            return this;
        }

        public InflectionBuilder SetDegree(Degree? degree)
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

        public static Person ParsePerson(char value)
        {
            switch (value)
            {
                case '1': return Person.First;
                case '2': return Person.Second;
                case '3': return Person.Third;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Tense ParseTense(char value)
        {
            switch (value)
            {
                case 'P': return Tense.Present;
                case 'I': return Tense.Imperfect;
                case 'F': return Tense.Future;
                case 'A': return Tense.Aorist;
                case 'X': return Tense.Perfect;
                case 'Y': return Tense.Pluperfect;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Voice ParseVoice(char value)
        {
            switch (value)
            {
                case 'A': return Voice.Active;
                case 'M': return Voice.Middle;
                case 'P': return Voice.Passive;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Mood ParseMood(char value)
        {
            switch (value)
            {
                case 'I': return Mood.Indicative;
                case 'D': return Mood.Imperative;
                case 'S': return Mood.Subjunctive;
                case 'O': return Mood.Optative;
                case 'N': return Mood.Infinitive;
                case 'P': return Mood.Participle;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Case ParseCase(char value)
        {
            switch (value)
            {
                case 'N': return Case.Nominative;
                case 'G': return Case.Genitive;
                case 'D': return Case.Dative;
                case 'A': return Case.Accusative;
                case 'V': return Case.V;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Number ParseNumber(char value)
        {
            switch (value)
            {
                case 'S': return Number.Singular;
                case 'P': return Number.Plural;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Gender ParseGender(char value)
        {
            switch (value)
            {
                case 'M': return Gender.Masculine;
                case 'F': return Gender.Feminine;
                case 'N': return Gender.Neuter;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }

        public static Degree ParseDegree(char value)
        {
            switch (value)
            {
                case 'C': return Degree.Comparative;
                case 'S': return Degree.Superlative;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(value));
            }
        }
    }
}
