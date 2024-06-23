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

        public PartsOfSpeech PartOfSpeech { get; init; }

        private readonly List<Form> m_forms = [];

        public Form GetForm(string word, Inflection inflection)
        {
            ArgumentNullException.ThrowIfNull(word, nameof(word));

            var form = m_forms.Where(form => form.Word == word && form.Inflection == inflection).SingleOrDefault();
            if (form == null)
            {
                form = new Form()
                {
                    Word = word,
                    Inflection = inflection
                };

                m_forms.Add(form);
            }
            //else
            //{
            //    if (form.Inflection != inflection)
            //    {
            //        throw new ArgumentOutOfRangeException(nameof(inflection), form.Inflection, "Existing form specifies different inflection.");
            //    }
            //}

            return form;
        }
    }
}
