using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class ExerciseCategory
    {
        public required string Name { get; init; }

        public List<IExerciseFactory> ExerciseFactories { get; } = [];
    }
}
