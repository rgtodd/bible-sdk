using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    internal class Question(string text, Answer[] answers)
    {
        public string Text => text;

        public Answer[] Answers => answers;
    }
}
