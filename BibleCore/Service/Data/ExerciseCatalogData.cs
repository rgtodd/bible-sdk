using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class ExerciseCatalogData
    {
        public required ExerciseCategoryData[] Categories { get; init; }
    }
}
