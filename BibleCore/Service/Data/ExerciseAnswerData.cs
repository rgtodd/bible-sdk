using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service.Data
{
    public class ExerciseAnswerData
    {
        public required string Answer { get; init; }

        public required bool IsCorrect { get; init; }
    }
}
