namespace BibleCore.Greek.Study
{
    internal class Exercise(string categoryName, string name, Question[] questions)
    {
        public string CategoryName => categoryName;

        public string Name => name;

        public Question[] Questions => questions;
    }
}
