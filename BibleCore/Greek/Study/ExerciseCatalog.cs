namespace BibleCore.Greek.Study
{
    internal class ExerciseCatalog(IExerciseFactory[] exerciseFactories, ThirdPartyWordList[] thirdPartyWordLists)
    {
        public IExerciseFactory[] ExerciseFactories => exerciseFactories;

        public ThirdPartyWordList[] ThirdPartyWordLists => thirdPartyWordLists;

        public IExerciseFactory GetExerciseFactory(string name)
        {
            return ExerciseFactories.Where(c => c.Name == name).Single();
        }
    }
}
