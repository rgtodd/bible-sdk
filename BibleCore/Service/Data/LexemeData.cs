namespace BibleCore.Service.Data
{
    public class LexemeData
    {
        public required string Lemma { get; init; }

        public required string LemmaTransliteration { get; init; }

        public required PartOfSpeechData PartOfSpeech { get; init; }

        public required string PartOfSpeechDescription { get; init; }

        public required string FullCitationForm { get; init; }

        public required string Gloss { get; init; }

        public required int[] StrongsNumber { get; init; }

        public required int[] GkNumber { get; init; }

        public required int MounceChapterNumber { get; init; }

        public required string MounceMorphcat { get; init; }

        public required FormData[] Forms { get; init; }
    }
}
