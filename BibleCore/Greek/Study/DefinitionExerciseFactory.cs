using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class DefinitionExerciseFactory(Lexicon lexicon, int mounceChapterNumber) : IExerciseFactory
    {
        public string Name => $"Mounce {mounceChapterNumber}";

        public Exercise CreateExercise()
        {
            throw new NotImplementedException();
        }

    }
}
