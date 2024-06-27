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
        // GET api/<LexemeController>/5
        [HttpGet()]
        public TextData? Get()
        {
            var textEntries = GlobalGreek.Instance.Text.Select();
            var textData = DataFactory.CreateTextData(textEntries);

            return textData;
        }
    }
}
