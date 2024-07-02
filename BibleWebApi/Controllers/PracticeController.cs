using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Code.Model;
using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWebApi.Controllers
{
    public class PracticeController(ILogger<PracticeModel> logger, IHttpClientFactory httpClientFactory) : Controller
    {
        private ILogger<PracticeModel> Logger { get; init; } = logger;

        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await Render();

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Update(PracticeModel model, string? word, string? option)
        {
            Logger.LogInformation("Word {word}, Option {option}", word, option);

            foreach (var exerciseWord in model?.ExerciseModel?.Words)
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

            return View("Index", model);

        }


        private async Task<PracticeModel?> Render()
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/PracticeApi/8";

            var c = HttpClientFactory.CreateClient();
            HttpResponseMessage response = await c.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var exerciseVocabularyData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<ExerciseVocabularyData>(json, Serialization.JsonSerializerOptions);
                var exerciseModel = ModelFactory.CreateExerciseModel(exerciseVocabularyData);
                return new PracticeModel()
                {
                    ExerciseModel = exerciseModel
                };
            }

            return null;
        }


    }
}
