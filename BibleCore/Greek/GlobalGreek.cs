using BibleCore.Greek.SblGnt;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Greek
{
    public sealed class GlobalGreek
    {
        // The volatile keyword ensures that the instantiation is complete 
        // before it can be accessed further helping with thread safety.
        //
        private static volatile GlobalGreek? m_instance;
        private static readonly object m_lock = new();

        private GlobalGreek()
        {
        }

        public required Text Text { get; init; }

        public required Lexicon Lexicon { get; init; }

        //uses a pattern known as double check locking
        public static GlobalGreek Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lock)
                    {
                        if (m_instance == null)
                        {
                            var text = new Text();
                            var lexicon = new Lexicon();

                            MorphGntFileParser.Parse(text, lexicon);
                            MorphGntLexemeParser.Parse(lexicon);

                            m_instance = new GlobalGreek()
                            {
                                Text = text,
                                Lexicon = lexicon
                            };
                        }
                    }
                }

                return m_instance;
            }
        }
    }
}
