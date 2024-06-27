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
        public static LexemeData CreateLexemeData(Lexeme lexeme)
        {
            return new LexemeData()
            {
                Lemma = lexeme.Lemma,
                PartOfSpeech = CreatePartOfSpeechData(lexeme.PartOfSpeech),
                FullCitationForm = lexeme.FullCitationForm ?? string.Empty,
                Gloss = lexeme.Gloss ?? string.Empty,
                Gk = lexeme.Gk,
                Strongs = lexeme.Strongs,
                Forms = CreateFormDataArray(lexeme.Forms)
            };
        }

        public static ReferenceData CreateReferenceData(Reference reference)
        {
            return new ReferenceData()
            {
                Bookmark = reference.Bookmark.ToString() ?? string.Empty,
                Reference = $"{DataFormatter.FormatBook(reference.Bookmark.Book)} {reference.Bookmark.Chapter}:{reference.Bookmark.Verse}"
            };
        }

        public static ReferenceData[] CreateReferenceDataArray(IEnumerable<Reference> references)
        {
            return references.Select(CreateReferenceData).ToArray();
        }

        public static FormData CreateFormData(Form form)
        {
            return new FormData()
            {
                Word = form.Word,
                Inflection = CreateInflectionData(form.Inflection),
                References = CreateReferenceDataArray(form.References)
            };
        }

        public static FormData[] CreateFormDataArray(IEnumerable<Form> forms)
        {
            return forms.Select(CreateFormData).ToArray();
        }

        public static CaseData? CreateCaseData(Case? _case)
        {
            return _case switch
            {
                null => null,
                Case.Accusative => CaseData.Accusative,
                Case.Dative => CaseData.Dative,
                Case.Genitive => CaseData.Genitive,
                Case.Nominative => CaseData.Nominative,
                Case.V => CaseData.V,
                _ => throw new NotImplementedException()
            };
        }

        public static DegreeData? CreateDegreeData(Degree? degree)
        {
            return degree switch
            {
                null => null,
                Degree.Comparative => DegreeData.Comparative,
                Degree.Superlative => DegreeData.Superlative,
                _ => throw new NotImplementedException()
            };
        }

        public static GenderData? CreateGenderData(Gender? gender)
        {
            return gender switch
            {
                null => null,
                Gender.Feminine => GenderData.Feminine,
                Gender.Masculine => GenderData.Masculine,
                Gender.Neuter => GenderData.Neuter,
                _ => throw new NotImplementedException()
            };
        }

        public static MoodData? CreateMoodData(Mood? mood)
        {
            return mood switch
            {
                null => null,
                Mood.Imperative => MoodData.Imperative,
                Mood.Indicative => MoodData.Indicative,
                Mood.Infinitive => MoodData.Infinitive,
                Mood.Optative => MoodData.Optative,
                Mood.Participle => MoodData.Participle,
                Mood.Subjunctive => MoodData.Subjunctive,
                _ => throw new NotImplementedException()
            };
        }

        public static NumberData? CreateNumberData(Number? number)
        {
            return number switch
            {
                null => null,
                Number.Plural => NumberData.Plural,
                Number.Singular => NumberData.Singular,
                _ => throw new NotImplementedException()
            };
        }

        public static PartOfSpeechData CreatePartOfSpeechData(PartOfSpeech partOfSpeech)
        {
            return partOfSpeech switch
            {
                PartOfSpeech.Adjective => PartOfSpeechData.Adjective,
                PartOfSpeech.Adverb => PartOfSpeechData.Adverb,
                PartOfSpeech.Conjunction => PartOfSpeechData.Conjunction,
                PartOfSpeech.DefiniteArticle => PartOfSpeechData.DefiniteArticle,
                PartOfSpeech.DemonstrativePronoun => PartOfSpeechData.DemonstrativePronoun,
                PartOfSpeech.IndefinitePronoun => PartOfSpeechData.IndefinitePronoun,
                PartOfSpeech.Interjection => PartOfSpeechData.Interjection,
                PartOfSpeech.Noun => PartOfSpeechData.Noun,
                PartOfSpeech.Particle => PartOfSpeechData.Particle,
                PartOfSpeech.PersonalPronoun => PartOfSpeechData.PersonalPronoun,
                PartOfSpeech.Preposition => PartOfSpeechData.Preposition,
                PartOfSpeech.RelativePronoun => PartOfSpeechData.RelativePronoun,
                PartOfSpeech.Verb => PartOfSpeechData.Verb,
                _ => throw new NotImplementedException()
            };
        }

        public static PersonData? CreatePersonData(Person? person)
        {
            return person switch
            {
                null => null,
                Person.First => PersonData.First,
                Person.Second => PersonData.Second,
                Person.Third => PersonData.Third,
                _ => throw new NotImplementedException()
            };
        }

        public static TenseData? CreateTenseData(Tense? tense)
        {
            return tense switch
            {
                null => null,
                Tense.Aorist => TenseData.Aorist,
                Tense.Future => TenseData.Future,
                Tense.Imperfect => TenseData.Imperfect,
                Tense.Perfect => TenseData.Perfect,
                Tense.Pluperfect => TenseData.Pluperfect,
                Tense.Present => TenseData.Present,
                _ => throw new NotImplementedException()
            };
        }

        public static VoiceData? CreateVoiceData(Voice? voice)
        {
            return voice switch
            {
                null => null,
                Voice.Active => VoiceData.Active,
                Voice.Middle => VoiceData.Middle,
                Voice.Passive => VoiceData.Passive,
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
