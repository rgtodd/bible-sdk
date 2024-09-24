namespace BibleCore.Greek.Study
{
    internal class Question(string text, string[] detail, Answer[] answers, int? strongsNumber, int? gkNumber)
    {
        public string Text => text;

        public string[] Detail => detail;

        public Answer[] Answers => answers;

        public int? StrongsNumber => strongsNumber;

        public int? GkNumber => gkNumber;


    }
}
