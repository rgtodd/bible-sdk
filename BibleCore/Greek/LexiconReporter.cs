using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public static class LexiconReporter
    {
        public static List<string> GetAllInflections(Lexicon lexicon)
        {
            var inflections = new HashSet<string>();
            foreach (var lexeme in lexicon.Lexemes)
            {
                foreach (var form in lexeme.Forms)
                {
                    inflections.Add(form.Inflection.ToString());
                }
            }

            var sortedInflections = new List<string>(inflections.Select(i => i.ToString()));
            sortedInflections.Sort();

            return sortedInflections;
        }
    }
}
