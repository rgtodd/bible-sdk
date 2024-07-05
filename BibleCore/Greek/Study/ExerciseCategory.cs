namespace BibleCore.Greek.Study
{
    internal class ExerciseCategory(string name, IExerciseFactory[] exerciseFactories)
    {
        public readonly static string PRONOUNCIATIONS = "Pronounciations";
        public readonly static string DEFINITIONS = "Definitions";
        public readonly static string PARTS_OF_SPEECH = "Parts of Speech";

        public string Name => name;

        public IExerciseFactory[] ExerciseFactories => exerciseFactories;

        public IExerciseFactory GetExerciseFactory(string name)
        {
            return exerciseFactories.Where(ef => ef.Name == name).Single();
        }
    }
}
