﻿using BibleCore.Greek;
using BibleCore.Greek.SblGnt;
using BibleCore.Greek.Study;

namespace BibleCore.Service
{
    internal sealed class GlobalGreek : IGlobalGreek
    {
        // The volatile keyword ensures that the instantiation is complete 
        // before it can be accessed further helping with thread safety.
        //
        private volatile bool m_isLoaded = false;
        private readonly object m_lock = new();

        private Text? m_text;
        private Lexicon? m_lexicon;
        private ExerciseCatalog? m_exerciseCatalog;

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

        public ExerciseCatalog ExerciseCatalog
        {
            get
            {
                EnsureLoaded();
                ArgumentNullException.ThrowIfNull(m_exerciseCatalog);
                return m_exerciseCatalog;
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
                        LoadLexicon();
                        LoadExerciseCatalog();

                        m_isLoaded = true;
                    }
                }
            }
        }

        private void LoadLexicon()
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
        }

        private void LoadExerciseCatalog()
        {
            ArgumentNullException.ThrowIfNull(m_lexicon);

            var exerciseCatalog = new ExerciseCatalog([
                new ExerciseCategory(
                    ExerciseCategory.DEFINITIONS,
                    [new DefinitionExerciseFactory(m_lexicon, 8)])
                ]);

            m_exerciseCatalog = exerciseCatalog;
        }
    }
}