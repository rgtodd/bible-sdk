using BibleCore.Greek;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexemeController : ControllerBase
    {
        // GET api/<LexemeController>/5
        [HttpGet("{id}")]
        public LexemeData? Get(int id)
        {
            var lexeme = GlobalGreek.Instance.Lexicon.Lexemes.Where(l => l.Strongs.Contains(id)).FirstOrDefault();
            if (lexeme == null)
            {
                return null;
            }

            var lexemeData = DataFactory.CreateLexemeData(lexeme);
            return lexemeData;
        }
    }
}
