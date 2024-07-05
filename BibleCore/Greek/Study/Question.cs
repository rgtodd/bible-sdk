namespace BibleCore.Greek.Study
{
    internal class Question(string text, string[] detail, Answer[] answers)
    {
        public string Text => text;

        public string[] Detail => detail;

        public Answer[] Answers => answers;
    }
}
