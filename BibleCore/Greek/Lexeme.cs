namespace BibleCore.Greek
{
    internal class Lexeme
    {
        public required string Lemma { get; init; }

        public required string LemmaTransliteration { get; init; }

        public PartOfSpeech PartOfSpeech { get; init; }

        public string FullCitationForm { get; set; } = string.Empty;

        public string Gloss { get; set; } = string.Empty;

        public int[] Strongs { get; set; } = [];

        public int[] Gk { get; set; } = [];

        public int MounceChapterNumber { get; set; } = 0;

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

        public Form GetOrCreateForm(Lexeme lexeme, string inflectedForm, Inflection inflection)
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
                    Inflection = inflection
                };

                Forms.Add(form);
            }

            return form;
        }
    }
}
