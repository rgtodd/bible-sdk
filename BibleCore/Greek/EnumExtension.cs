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
                PartOfSpeech.DemonstrativePronoun => "Pronoun - Demonstrative",
                PartOfSpeech.IndefinitePronoun => "Pronoun - Indefinite",
                PartOfSpeech.PersonalPronoun => "Pronoun - Personal",
                PartOfSpeech.RelativePronoun => "Pronoun - Relative",
                PartOfSpeech.Verb => "Verb",
                PartOfSpeech.Particle => "Particle",
                _ => "?",
            };
        }
    }
}
