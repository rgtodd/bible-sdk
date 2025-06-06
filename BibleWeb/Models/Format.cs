﻿using BibleCore.Service.Data;

using System.Text;

namespace BibleWeb.Models
{
    public static class Format
    {
        public static string PartOfSpeech(PartOfSpeechData partOfSpeech)
        {
            return partOfSpeech switch
            {
                PartOfSpeechData.Adjective => "Adjective",
                PartOfSpeechData.Conjunction => "Conjunction",
                PartOfSpeechData.Adverb => "Adverb",
                PartOfSpeechData.Interjection => "Interjection",
                PartOfSpeechData.Noun => "Noun",
                PartOfSpeechData.Preposition => "Preposition",
                PartOfSpeechData.DefiniteArticle => "Article",
                PartOfSpeechData.DemonstrativePronoun => "Dem Pro",
                PartOfSpeechData.IndefinitePronoun => "Ind Pro",
                PartOfSpeechData.PersonalPronoun => "Per Pro",
                PartOfSpeechData.RelativePronoun => "Rel Pro",
                PartOfSpeechData.Verb => "Verb",
                PartOfSpeechData.Particle => "Particle",
                _ => "?",
            };

        }

        public static string Inflection(InflectionData inflection)
        {
            var sb = new StringBuilder();

            var prefix = string.Empty;
            if (inflection.Tense != null)
            {
                sb.Append(prefix);
                prefix = "-";

                sb.Append(ToCode(inflection.Tense));
            }
            if (inflection.Voice != null)
            {
                sb.Append(prefix);
                prefix = "-";

                sb.Append(ToCode(inflection.Voice));
            }
            if (inflection.Mood != null)
            {
                sb.Append(prefix);
                prefix = "-";

                sb.Append(ToCode(inflection.Mood));
            }

            if (prefix == "-")
            {
                prefix = " ";
            }

            if (inflection.Case != null)
            {
                sb.Append(prefix);
                prefix = "-";
                sb.Append(ToCode(inflection.Case));
            }

            if (inflection.Person != null)
            {
                sb.Append(prefix);
                prefix = "-";
                sb.Append(ToCode(inflection.Person));
            }

            if (inflection.Number != null)
            {
                sb.Append(prefix);
                prefix = "-";
                sb.Append(ToCode(inflection.Number));
            }

            if (inflection.Gender != null)
            {
                sb.Append(prefix);
                prefix = "-";
                sb.Append(ToCode(inflection.Gender));
            }

            if (inflection.Degree != null)
            {
                sb.Append(prefix);
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                prefix = "-";
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                sb.Append(ToCode(inflection.Degree));
            }

            return sb.ToString();
        }

        public static string TenseStem(TenseStemData tenseStem)
        {
            return tenseStem switch
            {
                TenseStemData.Present => "Present",
                TenseStemData.FutureActive => "Future Active",
                TenseStemData.AoristActive => "Aorist Active",
                TenseStemData.AoristPassive => "Aorist Passive",
                TenseStemData.PerfectActive => "Perfect Active",
                TenseStemData.PerfectPassive => "Perfect Passive",
                _ => "?",
            };
        }

        public static string PersonalEnding(PersonalEndingData personalEnding)
        {
            return personalEnding switch
            {
                PersonalEndingData.PrimaryActive => "Primary / Active",
                PersonalEndingData.PrimaryPassive => "Primary / Middle/Passive",
                PersonalEndingData.SecondaryActive => "Secondary / Active",
                PersonalEndingData.SecondaryPassive => "Secondary / Middle/Passive",
                PersonalEndingData.None => "None",
                _ => "?",
            };
        }

        public static string[] PersonalEndings(PersonalEndingData personalEnding)
        {
            return personalEnding switch
            {
                PersonalEndingData.PrimaryActive => ["-", "ς", "ι", "μεν", "τε", "νσι"],
                PersonalEndingData.PrimaryPassive => ["μαι", "σαι", "ται", "μεθα", "σθε", "νται"],
                PersonalEndingData.SecondaryActive => ["ν", "ς", "-", "μεν", "τε", "ν"],
                PersonalEndingData.SecondaryPassive => ["μην", "σο", "το", "μεθα", "σθε", "ντο"],
                PersonalEndingData.None => [],
                _ => [],
            };
        }

        private static string ToCode(PersonData? persons)
        {
            return persons switch
            {
                null => "---",
                PersonData.First => "1P",
                PersonData.Second => "2P",
                PersonData.Third => "3P",
                _ => "???"
            };
        }

        private static string ToCode(TenseData? tense)
        {
            return tense switch
            {
                null => "---",
                TenseData.Present => "PRE",
                TenseData.Imperfect => "IMP",
                TenseData.Future => "FUT",
                TenseData.Aorist => "AOR",
                TenseData.Perfect => "PER",
                TenseData.Pluperfect => "PLU",
                _ => "???"
            };
        }

        private static string ToCode(VoiceData? voice)
        {
            return voice switch
            {
                null => "---",
                VoiceData.Active => "ACT",
                VoiceData.Middle => "MID",
                VoiceData.Passive => "PAS",
                _ => "???"
            };
        }

        private static string ToCode(MoodData? mood)
        {
            return mood switch
            {
                null => "---",
                MoodData.Indicative => "IND",
                MoodData.Imperative => "IMP",
                MoodData.Subjunctive => "SUB",
                MoodData.Optative => "OPT",
                MoodData.Infinitive => "INF",
                MoodData.Participle => "PAR",
                _ => "???"
            };
        }

        private static string ToCode(CaseData? _case)
        {
            return _case switch
            {
                null => "---",
                CaseData.Nominative => "NOM",
                CaseData.Genitive => "GEN",
                CaseData.Dative => "DAT",
                CaseData.Accusative => "ACC",
                CaseData.Vocative => "VOC",
                _ => "???"
            };
        }

        private static string ToCode(NumberData? number)
        {
            return number switch
            {
                null => "---",
                NumberData.Singular => "S",
                NumberData.Plural => "P",
                _ => "???"
            };
        }

        private static string ToCode(GenderData? gender)
        {
            return gender switch
            {
                null => "---",
                GenderData.Masculine => "M",
                GenderData.Feminine => "F",
                GenderData.Neuter => "N",
                _ => "???"
            };
        }

        private static string ToCode(DegreeData? degree)
        {
            return degree switch
            {
                null => "---",
                DegreeData.Comparative => "COM",
                DegreeData.Superlative => "SUP",
                _ => "???"
            };
        }

        public static string Concatenate(IEnumerable<string> values)
        {
            var sb = new StringBuilder();

            string prefix = "";
            foreach (var value in values)
            {
                sb.Append(prefix);
                prefix = "<br />";

                sb.Append(value);

            }

            return sb.ToString();
        }
    }
}
