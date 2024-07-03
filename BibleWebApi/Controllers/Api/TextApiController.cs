using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextApiController(ITextService textService) : ControllerBase
    {
        [FromQuery(Name = "range")]
        public string? RangeParameter { get; set; }

        [HttpGet()]
        public TextData? Get()
        {
            return textService.GetText(RangeParameter);
        }
    }
}
