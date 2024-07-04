using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class Question(string text, string detail, Answer[] answers)
    {
        public string Text => text;

        public string Detail => detail;

        public Answer[] Answers => answers;
    }
}
