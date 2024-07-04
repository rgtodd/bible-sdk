using BibleCore.Properties;

using System.Diagnostics;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BibleCore.Greek.SblGnt
{
    internal static class MorphGntLexemeParser
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
                if (lexemes.TryGetValue(lexeme.Lemma, out var morphGntLexeme))
                {
                    string fullCitationForm = morphGntLexeme.FullCitationForm;

                    string gloss = "RGT";
                    if (morphGntLexeme.Gloss != null)
                    {
                        gloss = morphGntLexeme.GlossAsString;
                    }

                    if (gloss == "RGT")
                    {
                        Debug.WriteLine($"Gloss not specified for {morphGntLexeme}");
                    }

                    lexeme.FullCitationForm = fullCitationForm;
                    lexeme.Gloss = gloss;
                    lexeme.Strongs = morphGntLexeme.StrongsAsIntegers;
                    lexeme.Gk = morphGntLexeme.GkAsIntegers;

                    var normalizedLemma = Alphabet.RemoveAccents(lexeme.Lemma);

                    foreach (var form in lexeme.Forms)
                    {
                        var normalizedInflectedForm = Alphabet.RemoveAccents(form.InflectedForm);

                        var lcs = LongestCommonSubstrings(normalizedLemma, normalizedInflectedForm);
                        if (lcs.Length > 0)
                        {
                            var core = lcs[0];

                            var idxLemma = normalizedLemma.IndexOf(core);
                            var lemmaPrefixRemoved = idxLemma > 0;
                            var lemmaSuffixRemoved = idxLemma + core.Length < normalizedLemma.Length;

                            int idxWord = normalizedInflectedForm.IndexOf(core);

                            form.Prefix = form.InflectedForm[..idxWord];
                            if (lemmaPrefixRemoved && form.Prefix.Length == 0)
                            {
                                form.Prefix = "*";
                            }

                            form.Core = form.InflectedForm.Substring(idxWord, core.Length);

                            form.Suffix = form.InflectedForm[(idxWord + core.Length)..];
                            if (lemmaSuffixRemoved && form.Suffix.Length == 0)
                            {
                                form.Suffix = "*";
                            }
                        }
                        else // Exception scenarios
                        {
                            form.Core = form.InflectedForm;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{lexeme.Lemma} lemma not found");
                }
            }
        }

        public static string[] LongestCommonSubstrings(string s, string t)
        {
            var l = new int[s.Length, t.Length];
            var z = 0;
            var result = new HashSet<string>();

            for (int i = 0; i < s.Length; ++i)
            {
                for (int j = 0; j < t.Length; ++j)
                {
                    if (s[i] == t[j])
                    {
                        l[i, j] = i == 0 || j == 0 ? 1 : l[i - 1, j - 1] + 1;

                        if (l[i, j] > z)
                        {
                            z = l[i, j];
                            result.Clear();
                            result.Add(s[(i - z + 1)..(i + 1)]);
                        }
                        if (l[i, j] == z)
                        {
                            result.Add(s[(i - z + 1)..(i + 1)]);
                        }
                    }
                    else
                    {
                        l[i, j] = 0;
                    }
                }
            }

            return [.. result];
        }
    }
}
