﻿namespace BibleCore.Greek
{
    internal class TextEntry
    {
        public required Bookmark Bookmark { get; init; }

        public required byte Position { get; init; }

        public required string Text { get; init; }

        public required string Word { get; init; }

        public required string NormalizedWord { get; init; }

        public required string TransliteratedWord { get; init; }

        public required PartOfSpeech PartOfSpeech { get; init; }

        public required Inflection Inflection { get; init; }

        public required Lexeme Lexeme { get; init; }

    }
}
