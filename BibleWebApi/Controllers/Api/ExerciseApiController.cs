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
        [HttpGet("{id}")]
        public ExerciseVocabularyData Get(int id)
        {
            return exerciseService.GetExerciseByMounceChapterNumber(id);
        }
    }
}
