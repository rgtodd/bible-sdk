﻿using BibleCore.Service.Data;

namespace BibleCore.Service
{
    public interface IExerciseService
    {
        ExerciseCatalogData GetExerciseCatalog();

        ExerciseData GetExercise(string name, string? wordListId, string? rangeExpression);
    }
}