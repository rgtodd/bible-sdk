using System.Text;

namespace BibleCore.Greek.SblGnt
{
    internal class MorphGntLexeme
    {
        public string? Pos { get; set; }

        public string? FullCitationForm { get; set; }

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
                        throw new Exception($"Gloss not specified for {FullCitationForm}"); ;

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
                return Strongs switch
                {
                    null => [],
                    int intValue => [intValue],
                    string stringValue => [int.Parse(stringValue)],
                    List<object> listValue => listValue.Select(o => int.Parse(o.ToString() ?? "")).ToArray(),
                    _ => throw new Exception($"Unknown Strongs type {Strongs.GetType()}"),
                };
            }
        }

        public int[] GkAsIntegers
        {
            get
            {
                return Gk switch
                {
                    null => [],
                    int intValue => [intValue],
                    string stringValue => [int.Parse(stringValue)],
                    List<object> listValue => listValue.Select(o => int.Parse(o.ToString() ?? "")).ToArray(),
                    _ => throw new Exception($"Unknown Gk type {Gk.GetType()}"),
                };
            }
        }

        public override string? ToString()
        {
            return $"{Pos}-{FullCitationForm}-{BdagHeadword}-{DankerEntry}-{DodsonEntry}-{MounceHeadword}-{Strongs}-{Gk}-{DodsonPos}-{Gloss}-{MounceMorphcat}";
        }
    }
}
