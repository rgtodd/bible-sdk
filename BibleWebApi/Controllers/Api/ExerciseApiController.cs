using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibleWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseApiController(IExerciseService exerciseService) : ControllerBase
    {
        [HttpGet("catalog")]
        public ExerciseCatalogData GetExerciseCatalog()
        {
            return exerciseService.GetExerciseCatalog();
        }

        [HttpGet("exercise")]
        public ExerciseData GetExercise(string categoryName, string name)
        {
            return exerciseService.GetExercise(categoryName, name);
        }
    }
}
