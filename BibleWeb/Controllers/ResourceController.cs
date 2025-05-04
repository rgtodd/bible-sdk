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

        public async Task<IActionResult> Vocabulary(string? range)
        {
            LexemeListModel model = range != null
                ? await GetLexemes(null, null, range)
                : await GetLexemes(1, 99, null);

            return View("Vocabulary", model);
        }

        public async Task<IActionResult> Verb(string? range, string? anchor)
        {
            VerbClassificationModel model = range != null
                ? await GetVerbs(null, null, range, anchor)
                : await GetVerbs(1, 99, null, anchor);

            return View("Verb", model);
        }

        public async Task<IActionResult> PrintVerb(string? range, string? anchor)
        {
            VerbClassificationModel model = range != null
                ? await GetVerbs(null, null, range, anchor)
                : await GetVerbs(1, 99, null, anchor);

            return View("PrintVerb", model);
        }

        private async Task<LexemeListModel> GetLexemes(int? minimumMounceNumber, int? maximumMounceNumber, string? rangeExpression)
        {
            List<LexemeData> lexemeData = await GetLexemeData(minimumMounceNumber, maximumMounceNumber, rangeExpression);

            var sortedLexemes = lexemeData
                .OrderBy(l => l.PartOfSpeechDescription)
                //.ThenBy(l => l.MounceMorphcat)
                .ThenBy(l => l.FullCitationForm)
                .ToList();

            var model = ModelFactory.CreateLexemeListModel(sortedLexemes, rangeExpression);

            return model;
        }

        private async Task<VerbClassificationModel> GetVerbs(int? minimumMounceNumber, int? maximumMounceNumber, string? rangeExpression, string? anchor)
        {
            List<LexemeData> lexemeData = await GetLexemeData(minimumMounceNumber, maximumMounceNumber, rangeExpression);

            var sortedLexemes = lexemeData
                .OrderBy(l => l.MounceMorphcat)
                .ThenBy(l => l.FullCitationForm)
                .ToList();

            var model = ModelFactory.CreateVerbClassificationModel(sortedLexemes, anchor);

            return model;
        }

        private async Task<List<LexemeData>> GetLexemeData(int? minimumMounceNumber, int? maximumMounceNumber, string? rangeExpression)
        {
            List<LexemeData>? lexemeData = null;

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/list?minimumMounceNumber={minimumMounceNumber}&maximumMounceNumber={maximumMounceNumber}&rangeExpression={rangeExpression}";

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
