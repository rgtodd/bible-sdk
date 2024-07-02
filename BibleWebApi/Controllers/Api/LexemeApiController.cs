using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexemeApiController(ILexemeService lexemeService) : ControllerBase
    {
        [HttpGet("{id}")]
        public LexemeData? Get(int id)
        {
            return lexemeService.GetByStrongsNumber(id);
        }
    }
}
