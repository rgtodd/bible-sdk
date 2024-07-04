namespace BibleCore.Greek.Study
{
    internal class PracticeWord
    {
        private readonly Lexeme m_lexeme;
        private readonly Dictionary<Mastery, int> m_masteries;
        private readonly string[] m_glosses;
        private readonly PartOfSpeech[] m_partsOfSpeech;

        public PracticeWord(Lexeme lexeme, IEnumerable<string> glosses, IEnumerable<PartOfSpeech> partsOfSpeech)
        {
            m_lexeme = lexeme;
            m_masteries = [];
            foreach (var mastery in Enum.GetValues<Mastery>())
            {
                m_masteries.Add(mastery, 0);
            }
            m_glosses = glosses.ToArray();
            m_partsOfSpeech = partsOfSpeech.ToArray();
        }

        public Lexeme Lexeme => m_lexeme;

        public Dictionary<Mastery, int> Masteries => m_masteries;

        public string[] Glosses => m_glosses;

        public PartOfSpeech[] PartsOfSpeech => m_partsOfSpeech;
    }
}
