using System.Globalization;
using System.Text;

namespace BibleCore.Greek
{
    public class Alphabet(Letter letter,
                          char lowerCase,
                          char upperCase,
                          char? alternateLowerCase,
                          string lowerTransliteration,
                          string upperTransliteration,
                          char keyStroke)
    {
        private readonly static Dictionary<Letter, Alphabet> s_alphabet;
        private readonly static Dictionary<char, Letter> s_letterByLowerCase;
        private readonly static Dictionary<char, Letter> s_letterByUpperCase;

        static Alphabet()
        {
            s_alphabet = new()
            {
                { Letter.Alpha,   new Alphabet(Letter.Alpha,   'α', 'Α', null, "a",  "A",  'A') },
                { Letter.Beta,    new Alphabet(Letter.Beta,    'β', 'Β', null, "b",  "B",  'B') },
                { Letter.Gamma,   new Alphabet(Letter.Gamma,   'γ', 'Γ', null, "g",  "G",  'G') }, // (n before γ, κ, ξ, and χ)
                { Letter.Delta,   new Alphabet(Letter.Delta,   'δ', 'Δ', null, "d",  "D",  'D') },
                { Letter.Epsilon, new Alphabet(Letter.Epsilon, 'ε', 'Ε', null, "e",  "E",  'E') },
                { Letter.Zeta,    new Alphabet(Letter.Zeta,    'ζ', 'Ζ', null, "z",  "Z",  'Z') },
                { Letter.Eta,     new Alphabet(Letter.Eta,     'η', 'Η', null, "ē",  "Ē",  'H') },
                { Letter.Theta,   new Alphabet(Letter.Theta,   'θ', 'Θ', null, "th", "Th", 'U') },
                { Letter.Iota,    new Alphabet(Letter.Iota,    'ι', 'Ι', null, "i",  "I",  'I') },
                { Letter.Kappa,   new Alphabet(Letter.Kappa,   'κ', 'Κ', null, "k",  "K",  'K') },
                { Letter.Lambda,  new Alphabet(Letter.Lambda,  'λ', 'Λ', null, "l",  "L",  'L') },
                { Letter.Mu,      new Alphabet(Letter.Mu,      'μ', 'Μ', null, "m",  "M",  'M') },
                { Letter.Nu,      new Alphabet(Letter.Nu,      'ν', 'Ν', null, "n",  "N",  'N') },
                { Letter.Xi,      new Alphabet(Letter.Xi,      'ξ', 'Ξ', null, "x",  "X",  'J') },
                { Letter.Omicron, new Alphabet(Letter.Omicron, 'ο', 'Ο', null, "o",  "O",  'O') },
                { Letter.Pi,      new Alphabet(Letter.Pi,      'π', 'Π', null, "p",  "P",  'P') },
                { Letter.Rho,     new Alphabet(Letter.Rho,     'ρ', 'Ρ', null, "r",  "R",  'R') },
                { Letter.Sigma,   new Alphabet(Letter.Sigma,   'σ', 'Σ', 'ς',  "s",  "S",  'S') },
                { Letter.Tau,     new Alphabet(Letter.Tau,     'τ', 'Τ', null, "t",  "T",  'T') },
                { Letter.Upsilon, new Alphabet(Letter.Upsilon, 'υ', 'Υ', null, "u",  "U",  'Y') }, // (u in diphthongs αυ, ευ, ηυ, ου, υι, and ωυ)/u
                { Letter.Phi,     new Alphabet(Letter.Phi,     'φ', 'Φ', null, "ph", "Ph", 'F') },
                { Letter.Chi,     new Alphabet(Letter.Chi,     'χ', 'Χ', null, "ch", "Ch", 'X') },
                { Letter.Psi,     new Alphabet(Letter.Psi,     'ψ', 'Ψ', null, "ps", "Ps", 'C') },
                { Letter.Omega,   new Alphabet(Letter.Omega,   'ω', 'Ω', null, "ō",  "Ō",  'V') }
            };

            s_letterByLowerCase = [];
            s_letterByUpperCase = [];
            foreach (var entry in s_alphabet)
            {
                s_letterByLowerCase.Add(entry.Value.LowerCase, entry.Key);

                if (entry.Value.AlternateLowerCase.HasValue)
                {
                    s_letterByLowerCase.Add(entry.Value.AlternateLowerCase.Value, entry.Key);
                }

                s_letterByUpperCase.Add(entry.Value.UpperCase, entry.Key);
            }
        }

        public Letter Letter { get; init; } = letter;

        public char LowerCase { get; init; } = lowerCase;

        public char? AlternateLowerCase { get; init; } = alternateLowerCase;

        public char UpperCase { get; init; } = upperCase;

        public string LowerTransliteration { get; init; } = lowerTransliteration;

        public string UpperTransliteration { get; init; } = upperTransliteration;

        public char KeyStroke { get; init; } = keyStroke;

        public static string RemoveAccents(string value)
        {
            return string.Concat(value.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Normalize(NormalizationForm.FormC);
        }

        public static string Transliterate(string value)
        {
            var sb = new StringBuilder();

            value = RemoveAccents(value);
            foreach (char c in value)
            {
                if (s_letterByLowerCase.TryGetValue(c, out var lowerCaseLetter))
                {
                    sb.Append(s_alphabet[lowerCaseLetter].LowerTransliteration);
                }
                else if (s_letterByUpperCase.TryGetValue(c, out var upperCaseLetter))
                {
                    sb.Append(s_alphabet[upperCaseLetter].UpperTransliteration);
                }
                else
                {
                    sb.Append('?');
                }
            }

            return sb.ToString();
        }


    }
}
