using BibleCore.Properties;

using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.SblGnt
{
    public class SblGntFileParser
    {
        public Lexicon Parse()
        {
            var lexicon = new Lexicon();

            using var stream = Resources.ResourceManager.GetStream("_61_Mt_morphgnt");
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));

            using var reader = new StreamReader(stream);

            //var inflections = new HashSet<string>();

            string? line;
            int lineCount = 0;
            while ((line = reader.ReadLine()) != null)
            {
                ++lineCount;
                string[] fields = line.Split(' ');
                if (fields.Length != 7)
                {
                    Console.WriteLine(line);
                }

                string bookChapterVerse = fields[0];
                string partOfSpeechCode = fields[1];
                string parsingCode = fields[2];
                string text = fields[3];
                string word = fields[4];
                string normalizedWord = fields[5];
                string lemma = fields[6];

                var partOfSpeech = ParsePartOfSpeech(partOfSpeechCode);
                var inflection = new InflectionBuilder().ParseInflection(parsingCode).Build();
                var reference = new Reference() { Value = normalizedWord };

                var lexeme = lexicon.GetLexeme(lemma, partOfSpeech);
                var form = lexeme.GetForm(normalizedWord, inflection);
                form.References.Add(reference);

                //inflections.Add(partOfSpeech + '-' + inflection);

                //                *book / chapter / verse
                //* part of speech
                // *parsing code
                // * text(including punctuation)
                // *word(with punctuation stripped)
                // * normalized word
                // * lemma
            }

            Console.WriteLine(lineCount + "lines processed.");

            //var sortedInflections = new List<string>(inflections);
            //sortedInflections.Sort();

            //foreach (var inflection in sortedInflections)
            //{
            //    Console.WriteLine(inflection);
            //}

            return lexicon;
        }

        private PartsOfSpeech ParsePartOfSpeech(string value)
        {
            return value switch
            {
                "A-" => PartsOfSpeech.Adjective,
                "C-" => PartsOfSpeech.Conjunction,
                "D-" => PartsOfSpeech.Adverb,
                "I-" => PartsOfSpeech.Interjection,
                "N-" => PartsOfSpeech.Noun,
                "P-" => PartsOfSpeech.Preposition,
                "RA" => PartsOfSpeech.DefiniteArticle,
                "RD" => PartsOfSpeech.DemonstrativePronoun,
                "RI" => PartsOfSpeech.IndefinitePronoun,
                "RP" => PartsOfSpeech.PersonalPronoun,
                "RR" => PartsOfSpeech.RelativePronoun,
                "V-" => PartsOfSpeech.Verb,
                "X-" => PartsOfSpeech.Particle,
                _ => throw new NotImplementedException()
            };
        }
    }
}
