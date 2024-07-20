namespace BibleCore.Greek.Study
{
    internal class Exercise(string name, string wordListDescription, string? wordListId, string? range, Question[] questions)
    {
        public string Name => name;

        public string WordListDescription => wordListDescription;

        public string? WordListId => wordListId;

        public string? Range => range;

        public Question[] Questions => questions;
    }
}
