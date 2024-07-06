using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextApiController(ITextService textService) : ControllerBase
    {
        [HttpGet()]
        public TextData? Get(string range)
        {
            return textService.GetText(range);
        }
    }
}
