using BibleCore.Greek;
using BibleCore.Greek.Study;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        // GET api/<LexemeController>/5
        [HttpGet("{id}")]
        public ExerciseVocabularyData Get(int id)
        {
            var practiceVocabulary = PracticeVocabulary.Load(GlobalGreek.Instance.Lexicon);
            var exerciseVocabularyData = DataFactory.CreateExerciseVocabularyData(practiceVocabulary);

            return exerciseVocabularyData;
        }
    }
}
