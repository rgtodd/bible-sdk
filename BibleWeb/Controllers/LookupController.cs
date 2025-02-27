using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWeb.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWeb.Controllers
{
    public class LookupController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index(int? strongs, int? gk, string? range)
        {
            var model = await GetLookupModel(strongs, gk, range);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LookupModel model)
        {
            ModelState.Clear();

            model = await GetLookupModel(model.StrongsNumber, model.GkNumber, model.Range);

            return View("Index", model);
        }

        private async Task<LookupModel> GetLookupModel(int? strongs, int? gk, string? range)
        {
            LexemeData? lexemeData = null;

            if (strongs != null || gk != null)
            {
                var request = HttpContext.Request;
                string url = strongs != null
                    ? $"{request.Scheme}://{request.Host}/api/LexemeApi/strongs/{strongs}"
                    : $"{request.Scheme}://{request.Host}/api/LexemeApi/gk/{gk}";

                var c = HttpClientFactory.CreateClient();
                var response = await c.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.ReasonPhrase);
                }

                var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");
                if (!string.IsNullOrEmpty(json))
                {
                    lexemeData = JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");
                }
            }

            var model = ModelFactory.CreateLookupModel(lexemeData, strongs, gk, range);

            return model;
        }
    }
}
