using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseApiController(IExerciseService exerciseService) : ControllerBase
    {
        [HttpGet("catalog")]
        [ProducesResponseType<ExerciseCatalogData>(StatusCodes.Status200OK)]
        public IActionResult GetExerciseCatalog()
        {
            return Ok(exerciseService.GetExerciseCatalog());
        }

        [HttpGet("exercise")]
        [ProducesResponseType<ExerciseData>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        public IActionResult GetExercise(string name, string? wordListId, string? range)
        {
            try
            {
                var exerciseData = exerciseService.GetExercise(name, wordListId, range);
                return Ok(exerciseData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
