namespace BibleCore.Greek.Study
{
    internal class Exercise(string name, string wordListDescription, int mounceChapterNumber, Question[] questions)
    {
        public string Name => name;

        public string WordListDescription => wordListDescription;

        public int MounceChapterNumber => mounceChapterNumber;

        public Question[] Questions => questions;
    }
}
