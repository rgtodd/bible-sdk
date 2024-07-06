using BibleCore.Greek;
using BibleCore.Greek.SblGnt;
using BibleCore.Greek.Study;

using Microsoft.Extensions.Logging;

namespace BibleCore.Service
{
    internal sealed class GlobalGreek(ILogger<GlobalGreek> logger) : IGlobalGreek
    {
        // The volatile keyword ensures that the instantiation is complete 
        // before it can be accessed further helping with thread safety.
        //
        private volatile bool m_isLoaded = false;
        private readonly object m_lock = new();

        private Text? m_text;
        private Lexicon? m_lexicon;
        private ExerciseCatalog? m_exerciseCatalog;

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

            MorphGntFileParser.Parse(logger, text, lexicon);
            MorphGntLexemeParser.Parse(logger, lexicon);
            MounceParser.Parse(logger, lexicon);

            lexicon.Lexemes.GroupBy(l => l.MounceChapterNumber, l => l.MounceChapterNumber, (number, numbers) => new { Chapter = number, Count = numbers.Count() })
                           .OrderBy(l => l.Chapter)
                           .ToList()
                           .ForEach(i => logger.LogDebug("Chapter {key} contains {count} words.", i.Chapter, i.Count));

            m_text = text;
            m_lexicon = lexicon;
        }

        private void LoadExerciseCatalog()
        {
            ArgumentNullException.ThrowIfNull(m_lexicon);

            var mounceChapterNumbers = m_lexicon.Lexemes.Select(l => l.MounceChapterNumber).Distinct().Order().Where(n => n != 0);

            var pronounciationExerciseFactories = mounceChapterNumbers.Select(n => new PronounciationExerciseFactory(m_lexicon, ExerciseCategory.PRONOUNCIATIONS, n)).ToArray();
            var pronounciationExerciseCategory = new ExerciseCategory(ExerciseCategory.PRONOUNCIATIONS, pronounciationExerciseFactories);

            var definitionExerciseFactories = mounceChapterNumbers.Select(n => new DefinitionExerciseFactory(m_lexicon, ExerciseCategory.DEFINITIONS, n)).ToArray();
            var definitionExerciseCategory = new ExerciseCategory(ExerciseCategory.DEFINITIONS, definitionExerciseFactories);

            var partsOfSpeechExerciseFactories = mounceChapterNumbers.Select(n => new PartOfSpeechExerciseFactory(m_lexicon, ExerciseCategory.PARTS_OF_SPEECH, n)).ToArray();
            var partsOfSpeechExerciseCategory = new ExerciseCategory(ExerciseCategory.PARTS_OF_SPEECH, partsOfSpeechExerciseFactories);

            var exerciseCatalog = new ExerciseCatalog([pronounciationExerciseCategory, definitionExerciseCategory, partsOfSpeechExerciseCategory]);

            m_exerciseCatalog = exerciseCatalog;
        }
    }
}
