using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public class Lexeme
    {
        public required string Lemma { get; init; }

        public required string LemmaTransliteration { get; init; }

        public PartOfSpeech PartOfSpeech { get; init; }

        public string FullCitationForm { get; set; } = string.Empty;

        public string Gloss { get; set; } = string.Empty;

        public int[] Strongs { get; set; } = [];

        public int[] Gk { get; set; } = [];

        public List<Form> Forms { get; } = [];

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
