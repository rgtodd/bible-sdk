namespace BibleCore.Greek
{
    internal class Lexeme : IEquatable<Lexeme?>
    {
        public required string Lemma { get; init; }

        public required string LemmaTransliteration { get; init; }

        public PartOfSpeech PartOfSpeech { get; init; }

        public string FullCitationForm { get; set; } = string.Empty;

        public string Gloss { get; set; } = string.Empty;

        public int[] StrongsNumber { get; set; } = [];

        public int[] GkNumber { get; set; } = [];

        public int MounceChapterNumber { get; set; } = 0;

        public string MounceMorphcat { get; set; } = string.Empty;

        public List<Form> Forms { get; } = [];

        public string? NounForm
        {
            get
            {
                var nounForm = FullCitationForm;
                if (nounForm != null)
                {
                    var idx = nounForm.IndexOf(',');
                    nounForm = idx != -1 ? nounForm[(idx + 1)..].TrimStart() : null;
                }
                return nounForm;
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Lexeme);
        }

        public bool Equals(Lexeme? other)
        {
            return other is not null &&
                   FullCitationForm == other.FullCitationForm;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullCitationForm);
        }

        public Form GetOrCreateForm(Lexeme lexeme, string inflectedForm, string inflectedTransliteration, Inflection inflection)
        {
            ArgumentNullException.ThrowIfNull(lexeme, nameof(lexeme));
            ArgumentNullException.ThrowIfNull(inflectedForm, nameof(inflectedForm));

            var form = Forms.Where(form => form.InflectedForm == inflectedForm && form.Inflection == inflection).SingleOrDefault();
            if (form == null)
            {
                form = new Form()
                {
                    Lexeme = lexeme,
                    InflectedForm = inflectedForm,
                    InflectedTransliteration = inflectedTransliteration,
                    Inflection = inflection
                };

                Forms.Add(form);
            }

            return form;
        }

        public static bool operator ==(Lexeme? left, Lexeme? right)
        {
            return EqualityComparer<Lexeme>.Default.Equals(left, right);
        }

        public static bool operator !=(Lexeme? left, Lexeme? right)
        {
            return !(left == right);
        }
    }
}
