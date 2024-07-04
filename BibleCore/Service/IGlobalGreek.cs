using BibleCore.Greek;

namespace BibleCore.Service
{
    internal interface IGlobalGreek
    {
        Lexicon Lexicon { get; }

        Text Text { get; }
    }
}