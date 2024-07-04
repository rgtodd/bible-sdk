using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal interface IExerciseFactory
    {
        public string Name { get; }

        public Exercise CreateExercise();
    }
}
