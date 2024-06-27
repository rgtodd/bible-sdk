using BibleCore.Properties;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BibleCore.Greek.SblGnt
{
    public static class MorphGntLexemeParser
    {
        public static void Parse(Lexicon lexicon)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(HyphenatedNamingConvention.Instance)
                .Build();

            var lexemesYaml = Resources.lexemes;
            var lexemes = deserializer.Deserialize<Dictionary<string, MorphGntLexeme>>(lexemesYaml);

            foreach (var lexeme in lexicon.Lexemes)
            {
                if (lexeme.Lemma == "ἄπειμι")
                {
                    Debug.Print("ἄπειμι");
                }
                if (lexemes.TryGetValue(lexeme.Lemma, out var morphGntLexeme))
                {
                    lexeme.FullCitationForm = morphGntLexeme.FullCitationForm;
                    lexeme.Gloss = morphGntLexeme.GlossAsString;
                    lexeme.Strongs = morphGntLexeme.StrongsAsIntegers;
                    lexeme.Gk = morphGntLexeme.GkAsIntegers;
                }
                else
                {
                    Console.WriteLine($"{lexeme.Lemma} lemma not found");
                }
            }
        }
    }
}
