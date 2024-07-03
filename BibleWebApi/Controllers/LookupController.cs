using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWebApi.Controllers
{
    public class LookupController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var model = new LookupModel();

            if (id == null)
            {
                model.LexemeData = null;
                model.Message = null;
            }
            else
            {
                var request = HttpContext.Request;
                var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/{id.Value}";

                var c = HttpClientFactory.CreateClient();
                var response = await c.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.ReasonPhrase);
                }

                var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

                model.LexemeData = JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

                model.Message = model.LexemeData == null ? "Not found." : null;
                model.Strongs = id.Value.ToString();
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LookupModel model)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/{model.Strongs}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            model.LexemeData = JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            model.Message = model.LexemeData == null ? "Not found." : null;

            return View("Index", model);
        }
    }
}
