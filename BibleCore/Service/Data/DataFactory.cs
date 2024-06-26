using BibleCore.Greek;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public static class DataFactory
    {

        public static CaseData? CreateCaseData(Cases? _case)
        {
            return _case switch
            {
                null => null,
                Cases.Accusative => CaseData.Accusative,
                Cases.Dative => CaseData.Dative,
                Cases.Genitive => CaseData.Genitive,
                Cases.Nominative => CaseData.Nominative,
                Cases.V => CaseData.V,
                _ => throw new NotImplementedException()
            };
        }

        public static DegreeData? CreateDegreeData(Degrees? degree)
        {
            return degree switch
            {
                null => null,
                Degrees.Comparative => DegreeData.Comparative,
                Degrees.Superlative => DegreeData.Superlative,
                _ => throw new NotImplementedException()
            };
        }

        public static GenderData? CreateGenderData(Genders? gender)
        {
            return gender switch
            {
                null => null,
                Genders.Feminine => GenderData.Feminine,
                Genders.Masculine => GenderData.Masculine,
                Genders.Neuter => GenderData.Neuter,
                _ => throw new NotImplementedException()
            };
        }

        public static MoodData? CreateMoodData(Moods? mood)
        {
            return mood switch
            {
                null => null,
                Moods.Imperative => MoodData.Imperative,
                Moods.Indicative => MoodData.Indicative,
                Moods.Infinitive => MoodData.Infinitive,
                Moods.Optative => MoodData.Optative,
                Moods.Participle => MoodData.Participle,
                Moods.Subjunctive => MoodData.Subjunctive,
                _ => throw new NotImplementedException()
            };
        }

        public static NumberData? CreateNumberData(Numbers? number)
        {
            return number switch
            {
                null => null,
                Numbers.Plural => NumberData.Plural,
                Numbers.Singular => NumberData.Singular,
                _ => throw new NotImplementedException()
            };
        }

        public static PartOfSpeechData? CreatePartOfSpeechData(PartsOfSpeech? partOfSpeech)
        {
            return partOfSpeech switch
            {
                null => null,
                PartsOfSpeech.Adjective => PartOfSpeechData.Adjective,
                PartsOfSpeech.Adverb => PartOfSpeechData.Adverb,
                PartsOfSpeech.Conjunction => PartOfSpeechData.Conjunction,
                PartsOfSpeech.DefiniteArticle => PartOfSpeechData.DefiniteArticle,
                PartsOfSpeech.DemonstrativePronoun => PartOfSpeechData.DemonstrativePronoun,
                PartsOfSpeech.IndefinitePronoun => PartOfSpeechData.IndefinitePronoun,
                PartsOfSpeech.Interjection => PartOfSpeechData.Interjection,
                PartsOfSpeech.Noun => PartOfSpeechData.Noun,
                PartsOfSpeech.Particle => PartOfSpeechData.Particle,
                PartsOfSpeech.PersonalPronoun => PartOfSpeechData.PersonalPronoun,
                PartsOfSpeech.Preposition => PartOfSpeechData.Preposition,
                PartsOfSpeech.RelativePronoun => PartOfSpeechData.RelativePronoun,
                PartsOfSpeech.Verb => PartOfSpeechData.Verb,
                _ => throw new NotImplementedException()
            };
        }

        public static PersonData? CreatePersonData(Persons? person)
        {
            return person switch
            {
                null => null,
                Persons.First => PersonData.First,
                Persons.Second => PersonData.Second,
                Persons.Third => PersonData.Third,
                _ => throw new NotImplementedException()
            };
        }

        public static TenseData? CreateTenseData(Tenses? tense)
        {
            return tense switch
            {
                null => null,
                Tenses.Aorist => TenseData.Aorist,
                Tenses.Future => TenseData.Future,
                Tenses.Imperfect => TenseData.Imperfect,
                Tenses.Perfect => TenseData.Perfect,
                Tenses.Pluperfect => TenseData.Pluperfect,
                Tenses.Present => TenseData.Present,
                _ => throw new NotImplementedException()
            };
        }

        public static VoiceData? CreateVoiceData(Voices? voice)
        {
            return voice switch
            {
                null => null,
                Voices.Active => VoiceData.Active,
                Voices.Middle => VoiceData.Middle,
                Voices.Passive => VoiceData.Passive,
                _ => throw new NotImplementedException()
            };
        }

        public static InflectionData CreateInflectionData(Inflection inflection)
        {
            return new InflectionData
            {
                Case = CreateCaseData(inflection.Case),
                Degree = CreateDegreeData(inflection.Degree),
                Gender = CreateGenderData(inflection.Gender),
                Mood = CreateMoodData(inflection.Mood),
                Number = CreateNumberData(inflection.Number),
                Person = CreatePersonData(inflection.Person),
                Tense = CreateTenseData(inflection.Tense),
                Voice = CreateVoiceData(inflection.Voice)
            };
        }
    }
}
