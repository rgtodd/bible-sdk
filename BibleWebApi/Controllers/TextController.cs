using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController(ITextService textService) : ControllerBase
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
