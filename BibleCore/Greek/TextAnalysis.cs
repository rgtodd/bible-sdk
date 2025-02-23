namespace BibleCore.Greek
{
    internal class TextAnalysis
    {
        public required IDictionary<PartOfSpeech, int> PartOfSpeechCounts { get; init; }

        public required IDictionary<Case, IDictionary<Gender, int>> NounCounts { get; init; }

        public required IDictionary<Mood, IDictionary<Tense, IDictionary<Voice, int>>> VerbCounts { get; init; }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public class Builder
        {
            private readonly Dictionary<PartOfSpeech, int> partOfSpeechCounts = [];
            private readonly Dictionary<Case, IDictionary<Gender, int>> nounCounts = [];
            private readonly Dictionary<Mood, IDictionary<Tense, IDictionary<Voice, int>>> verbCounts = [];

            public void Add(PartOfSpeech partOfSpeech, Inflection inflection)
            {
                partOfSpeechCounts[partOfSpeech] = partOfSpeechCounts.TryGetValue(partOfSpeech, out var count) ? count + 1 : 1;

                if (partOfSpeech == PartOfSpeech.Noun)
                {
                    var _case = inflection.Case;
                    var gender = inflection.Gender;

                    if (!nounCounts.ContainsKey(_case))
                    {
                        nounCounts[_case] = new Dictionary<Gender, int>();
                    }
                    var genderDictionary = nounCounts[_case];

                    genderDictionary[gender] = genderDictionary.TryGetValue(gender, out var genderCount) ? genderCount + 1 : 1;
                }

                if (partOfSpeech == PartOfSpeech.Verb)
                {
                    var mood = inflection.Mood;
                    var tense = inflection.Tense;
                    var voice = inflection.Voice;

                    if (!verbCounts.ContainsKey(mood))
                    {
                        verbCounts[mood] = new Dictionary<Tense, IDictionary<Voice, int>>();
                    }
                    var tenseDictionary = verbCounts[mood];

                    if (!tenseDictionary.ContainsKey(tense))
                    {
                        tenseDictionary[tense] = new Dictionary<Voice, int>();
                    }
                    var voiceDictionary = tenseDictionary[tense];

                    voiceDictionary[voice] = voiceDictionary.TryGetValue(voice, out var voiceCount) ? voiceCount + 1 : 1;
                }
            }

            public void Add(IEnumerable<TextEntry> textEntries)
            {
                foreach (var textEntry in textEntries)
                {
                    Add(textEntry.PartOfSpeech, textEntry.Inflection);
                }
            }

            public TextAnalysis Build()
            {
                return new TextAnalysis
                {
                    PartOfSpeechCounts = partOfSpeechCounts,
                    NounCounts = nounCounts,
                    VerbCounts = verbCounts
                };
            }
        }
    }
}
