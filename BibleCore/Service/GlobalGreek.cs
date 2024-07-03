using BibleCore.Greek;
using BibleCore.Greek.SblGnt;

namespace BibleCore.Service
{
    public sealed class GlobalGreek : IGlobalGreek
    {
        // The volatile keyword ensures that the instantiation is complete 
        // before it can be accessed further helping with thread safety.
        //
        private volatile bool m_isLoaded = false;
        private readonly object m_lock = new();

        private Text? m_text;
        private Lexicon? m_lexicon;

        public GlobalGreek()
        {
        }

        public Text Text
        {
            get
            {
                EnsureLoaded();
                ArgumentNullException.ThrowIfNull(m_text);
                return m_text;
            }
        }

        public Lexicon Lexicon
        {
            get
            {
                EnsureLoaded();
                ArgumentNullException.ThrowIfNull(m_lexicon);
                return m_lexicon;
            }
        }

        private void EnsureLoaded()
        {
            if (!m_isLoaded)
            {
                lock (m_lock)
                {
                    if (!m_isLoaded)
                    {
                        var text = new Text();
                        var lexicon = new Lexicon();

                        MorphGntFileParser.Parse(text, lexicon);
                        MorphGntLexemeParser.Parse(lexicon);
                        FlashworksGreekParser.Parse(lexicon);

                        lexicon.Lexemes.GroupBy(l => l.MounceChapterNumber, l => l.MounceChapterNumber, (number, numbers) => new { Key = number, Count = numbers.Count() })
                                       .ToList()
                                       .ForEach(i => Console.WriteLine($"{i.Key} = {i.Count}."));

                        m_text = text;
                        m_lexicon = lexicon;
                        m_isLoaded = true;
                    }
                }
            }
        }
    }
}
