using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.SblGnt
{
    public class MorphGntLexeme
    {
        public string? Pos { get; set; }

        public string FullCitationForm { get; set; } = string.Empty;

        public string? BdagHeadword { get; set; }

        public string? DankerEntry { get; set; }

        public string? DodsonEntry { get; set; }

        public string? MounceHeadword { get; set; }

        public object? Strongs { get; set; }

        public object? Gk { get; set; }

        public string? DodsonPos { get; set; }

        public object? Gloss { get; set; }

        public object? MounceMorphcat { get; set; }

        public string GlossAsString
        {
            get
            {
                var sb = new StringBuilder();

                switch (Gloss)
                {
                    case null:
                        break;

                    case string stringValue:
                        sb.Append(stringValue);
                        break;

                    case List<object> listValue:
                        sb.Append(string.Join(" / ", listValue));
                        break;

                    default:
                        throw new Exception($"Unknown Gloss type {Gloss.GetType()}");
                }

                return sb.ToString();
            }
        }

        public int[] StrongsAsIntegers
        {
            get
            {
                switch (Strongs)
                {
                    case null: return [];

                    case int intValue:
                        return [intValue];

                    case string stringValue:
                        return [int.Parse(stringValue)];

                    case List<object> listValue:
                        return listValue.Select(o => int.Parse(o.ToString() ?? "")).ToArray();

                    default:
                        throw new Exception($"Unknown Strongs type {Strongs.GetType()}");
                }
            }
        }

        public int[] GkAsIntegers
        {
            get
            {
                switch (Gk)
                {
                    case null: return [];

                    case int intValue:
                        return [intValue];

                    case string stringValue:
                        return [int.Parse(stringValue)];

                    case List<object> listValue:
                        return listValue.Select(o => int.Parse(o.ToString() ?? "")).ToArray();

                    default:
                        throw new Exception($"Unknown Gk type {Gk.GetType()}");
                }
            }
        }
    }
}
