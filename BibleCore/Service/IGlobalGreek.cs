using BibleCore.Greek;
using BibleCore.Greek.Study;

namespace BibleCore.Service
{
    internal interface IGlobalGreek
    {
        Lexicon Lexicon { get; }

        Text Text { get; }

        ExerciseCatalog ExerciseCatalog { get; }
    }
}