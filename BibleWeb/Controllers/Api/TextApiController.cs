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

        [HttpGet("movePrevious")]
        public TextData? MovePrevious(string range)
        {
            return textService.MovePreviousText(range);
        }

        [HttpGet("moveNext")]
        public TextData? MoveNext(string range)
        {
            return textService.MoveNextText(range);
        }

        [HttpGet("extendPrevious")]
        public TextData? ExtendPrevious(string range)
        {
            return textService.ExtendPreviousText(range);
        }

        [HttpGet("extendNext")]
        public TextData? ExtendNext(string range)
        {
            return textService.ExtendNextText(range);
        }
    }
}
