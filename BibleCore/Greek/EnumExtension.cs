namespace BibleCore.Greek
{
    internal static class EnumExtension
    {
        public static string AsString(this PartOfSpeech partOfSpeech)
        {
            return partOfSpeech switch
            {
                PartOfSpeech.Adjective => "Adjective",
                PartOfSpeech.Conjunction => "Conjunction",
                PartOfSpeech.Adverb => "Adverb",
                PartOfSpeech.Interjection => "Interjection",
                PartOfSpeech.Noun => "Noun",
                PartOfSpeech.Preposition => "Preposition",
                PartOfSpeech.DefiniteArticle => "Definite Article",
                PartOfSpeech.DemonstrativePronoun => "Demonstrative Pronoun",
                PartOfSpeech.IndefinitePronoun => "Indefinite Pronoun",
                PartOfSpeech.PersonalPronoun => "Personal Pronoun",
                PartOfSpeech.RelativePronoun => "Relative Pronoun",
                PartOfSpeech.Verb => "Verb",
                PartOfSpeech.Particle => "Particle",
                _ => "?",
            };
        }
    }
}
