using BibleCore.Greek;

namespace BibleCore.Service
{
    public interface IGlobalGreek
    {
        Lexicon Lexicon { get; }

        Text Text { get; }
    }
}