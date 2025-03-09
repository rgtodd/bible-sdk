using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWeb.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWeb.Controllers
{
    public class ResourceController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        public IActionResult Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> Vocabulary()
        {
            var model = await GetLexemes(1, 99);

            return View("Vocabulary", model);
        }

        public async Task<IActionResult> Verb()
        {
            var model = await GetVerbs(1, 99);

            return View("Verb", model);
        }

        private async Task<LexemeListModel> GetLexemes(int? minimumMounceNumber, int? maximumMounceNumber)
        {
            List<LexemeData> lexemeData = await GetLexemeData(minimumMounceNumber, maximumMounceNumber);

            var sortedLexemes = lexemeData
                .OrderBy(l => l.PartOfSpeechDescription)
                //.ThenBy(l => l.MounceMorphcat)
                .ThenBy(l => l.FullCitationForm)
                .ToList();

            var model = ModelFactory.CreateLexemeListModel(sortedLexemes);

            return model;
        }

        private async Task<VerbClassificationModel> GetVerbs(int? minimumMounceNumber, int? maximumMounceNumber)
        {
            List<LexemeData> lexemeData = await GetLexemeData(minimumMounceNumber, maximumMounceNumber);

            var sortedLexemes = lexemeData
                .OrderBy(l => l.MounceMorphcat)
                .ThenBy(l => l.FullCitationForm)
                .ToList();

            var model = ModelFactory.CreateVerbClassificationModel(sortedLexemes);

            return model;
        }

        private async Task<List<LexemeData>> GetLexemeData(int? minimumMounceNumber, int? maximumMounceNumber)
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

            return lexemeData;
        }
    }
}
