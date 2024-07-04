using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class Answer(string text, bool isCorrect)
    {
        public string Text => text;

        public bool IsCorrect => isCorrect;
    }
}
