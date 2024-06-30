using BibleCore.Service.Data;

using BibleWebApi.Code;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;

using WordQuiz.Pages;

namespace BibleWebApi.Pages
{
    public class PracticeModel(ILogger<PracticeModel> logger, IHttpClientFactory httpClientFactory) : PageModel
    {
        private  ILogger<PracticeModel> Logger { get; init; } = logger;

        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        public PracticeVocabularyData? PracticeVocabularyData { get; set; }

        public async Task OnGet()
        {
            await Render();
        }

        private async Task Render()
        {
            var request = PageContext.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/practice/1";

            var c = HttpClientFactory.CreateClient();
            HttpResponseMessage response = await c.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                PracticeVocabularyData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<PracticeVocabularyData>(json, PageResources.JsonSerializerOptions);
            }
        }

        public async Task OnPost(string? lemma, string? gloss)
        {
            Logger.LogInformation("Lemma {lemma}, Gloss {gloss}", lemma, gloss);

            await Render();
        }

    }
}
