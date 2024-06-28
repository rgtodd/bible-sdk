using BibleCore.Greek;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        [FromQuery(Name = "range")]
        public string? RangeParameter { get; set; }

        // GET api/<LexemeController>/5
        [HttpGet()]
        public TextData? Get()
        {
            if (RangeParameter == null)
            {
                return null;
            }

            var range = BookmarkFactory.ParseRange(RangeParameter);
            if (range == null)
            {
                return null;
            }

            var textEntries = GlobalGreek.Instance.Text.Select(range, 500);

            var textData = DataFactory.CreateTextData(range, textEntries);

            return textData;
        }
    }
}
