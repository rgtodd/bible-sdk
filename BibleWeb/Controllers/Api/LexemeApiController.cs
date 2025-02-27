using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexemeApiController(ILexemeService lexemeService) : ControllerBase
    {
        [HttpGet("strongs/{id}")]
        public LexemeData? GetByStrongsNumber(int id)
        {
            return lexemeService.GetByStrongsNumber(id);
        }

        [HttpGet("gk/{id}")]
        public LexemeData? GetByGkNumber(int id)
        {
            return lexemeService.GetByGkNumber(id);
        }

        [HttpGet("list")]
        public List<LexemeData> GetLexemes(int? minimumMounceNumber, int? maximumMounceNumber)
        {
            return lexemeService.GetLexemes(minimumMounceNumber, maximumMounceNumber);
        }
    }
}
