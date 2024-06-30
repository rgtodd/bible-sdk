using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek.Study
{
    public class PracticeWord
    {
        private readonly Lexeme m_lexeme;
        private readonly Dictionary<Mastery, int> m_masteries;
        private readonly string[] m_glosses;
        private readonly PartOfSpeech[] m_partsOfSpeech;

        public PracticeWord(Lexeme lexeme)
        {
            m_lexeme = lexeme;
            m_masteries = [];
            foreach (var mastery in Enum.GetValues<Mastery>())
            {
                m_masteries.Add(mastery, 0);
            }
            m_glosses = ["this", "is", "a", "test"];
            m_partsOfSpeech = [PartOfSpeech.Noun, PartOfSpeech.Verb, PartOfSpeech.Adjective];
        }

        public Lexeme Lexeme => m_lexeme;

        public Dictionary<Mastery, int> Masteries => m_masteries;

        public string[] Glosses => m_glosses;

        public PartOfSpeech[] PartsOfSpeech => m_partsOfSpeech;
    }
}
