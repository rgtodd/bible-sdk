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
        private Apparatus? m_apparatus;
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

        public Apparatus Apparatus
        {
            get
            {
                EnsureLoaded();
                ArgumentNullException.ThrowIfNull(m_apparatus);
                return m_apparatus;
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
            var apparatus = new Apparatus();

            MorphGntFileParser.Parse(logger, text, lexicon);
            MorphGntLexemeParser.Parse(logger, lexicon);
            MounceParser.Parse(logger, lexicon);
            SblGntApparatusParser.Parse(logger, apparatus);

            lexicon.Lexemes.GroupBy(l => l.MounceChapterNumber, l => l.MounceChapterNumber, (number, numbers) => new { Chapter = number, Count = numbers.Count() })
                           .OrderBy(l => l.Chapter)
                           .ToList()
                           .ForEach(i => logger.LogDebug("Chapter {key} contains {count} words.", i.Chapter, i.Count));

            m_text = text;
            m_lexicon = lexicon;
            m_apparatus = apparatus;
        }

        private void LoadExerciseCatalog()
        {
            ArgumentNullException.ThrowIfNull(m_lexicon);

            IExerciseFactory[] exerciseFactories = [
                new PronounciationExerciseFactory(),
                new DefinitionExerciseFactory(),
                new PartOfSpeechExerciseFactory(),
                new VocabularyExerciseFactory()];

            var mounceChapterNumbers = m_lexicon.Lexemes.Select(l => l.MounceChapterNumber).Where(n => n != 0).Distinct().Order();
            var thirdPartyWordLists = mounceChapterNumbers.Select(m => new ThirdPartyWordList() { Name = $"Chapter {m}", WordListId = $"m-{m}" }).ToArray();

            var exerciseCatalog = new ExerciseCatalog(exerciseFactories, thirdPartyWordLists);

            m_exerciseCatalog = exerciseCatalog;
        }
    }
}
