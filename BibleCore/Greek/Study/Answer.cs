namespace BibleCore.Greek.Study
{
    internal class Answer(string text, bool isCorrect)
    {
        public string Text => text;

        public bool IsCorrect => isCorrect;
    }
}
