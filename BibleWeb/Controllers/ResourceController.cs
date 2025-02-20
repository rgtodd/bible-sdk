using BibleCore.Service.Data;
using BibleCore.Utility;


using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWeb.Controllers
{
    public class ResourceController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        public async Task<IActionResult> Index()
        {
            var model = await GetLexemes(1, 36);

            return View("Vocabulary", model);
        }

        private async Task<LexemeListModel> GetLexemes(int? minimumMounceNumber, int? maximumMounceNumber)
        {
            List<LexemeData>? lexemeData = null;

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/list?minimumMounceNumber={minimumMounceNumber}&maximumMounceNumber={maximumMounceNumber}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");
            if (!string.IsNullOrEmpty(json))
            {
                lexemeData = JsonSerializer.Deserialize<List<LexemeData>>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");
            }

            lexemeData ??= [];

            var model = ModelFactory.CreateLexemeListModel(lexemeData);

            return model;
        }

    }
}
