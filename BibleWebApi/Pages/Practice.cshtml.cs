using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Code;
using BibleWebApi.Code.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;

using WordQuiz.Pages;

namespace BibleWebApi.Pages
{
    public class PracticeModel(ILogger<PracticeModel> logger, IHttpClientFactory httpClientFactory) : PageModel
    {
        private ILogger<PracticeModel> Logger { get; init; } = logger;

        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [BindProperty]
        public ExerciseModel? ExerciseModel { get; set; }

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
                var exerciseVocabularyData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<ExerciseVocabularyData>(json, Serialization.JsonSerializerOptions);
                ExerciseModel = ModelFactory.CreateExerciseModel(exerciseVocabularyData);
            }
        }

        public void OnPost(string? word, string? option)
        {
            Logger.LogInformation("Word {word}, Option {option}", word, option);

            foreach (var exerciseWord in ExerciseModel!.Words)
            {
                if (exerciseWord.Word == word)
                {
                    foreach (var exerciseOption in exerciseWord.Options)
                    {
                        exerciseOption.IsSelected = exerciseOption.Option == option;
                    }
                    break;
                }

                //await Render();
            }

            ModelState.Clear();

        }
    }
}