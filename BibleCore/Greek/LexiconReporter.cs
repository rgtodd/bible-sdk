using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public static class LexiconReporter
    {
        public static void DumpInflections(Lexicon lexicon)
        {
            foreach (var inflection in LexiconReporter.GetAllInflections(lexicon))
            {
                Console.WriteLine(inflection);
            }
        }

        public static void Dump(Lexicon lexicon)
        {
            var lexemes = lexicon.Lexemes.OrderBy(l => l.Lemma).Select(l => l);
            foreach (var lexeme in lexemes)
            {
                Console.WriteLine(lexeme.Lemma);
            }
        }

        public static void DumpReferences(Lexicon lexicon)
        {
            var references = lexicon.Lexemes.SelectMany(l => l.Forms).SelectMany(f => f.Bookmarks).Count();
            Console.WriteLine($"Reference count = {references}");
        }

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
