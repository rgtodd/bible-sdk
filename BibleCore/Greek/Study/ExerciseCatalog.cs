using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class ExerciseCatalog(ExerciseCategory[] categories)
    {
        public ExerciseCategory[] Categories => categories;

        public ExerciseCategory GetCategory(string name)
        {
            return categories.Where(c => c.Name == name).Single();
        }
    }
}
