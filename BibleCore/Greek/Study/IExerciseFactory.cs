namespace BibleCore.Greek.Study
{
    internal interface IExerciseFactory
    {
        public string Name { get; }

        public Exercise CreateExercise();
    }
}
