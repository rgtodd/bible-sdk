using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWebApi.Controllers
{
    public class ExerciseController(ILogger<ExerciseController> logger, IHttpClientFactory httpClientFactory) : Controller
    {
        private ILogger<ExerciseController> Logger { get; init; } = logger;

        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await GetExerciseModel(8);

            return View("Exercise", model);
        }

        [HttpPost]
        public IActionResult Update(ExerciseDataModel data, string? word, string? option)
        {
            Logger.LogInformation("Word {word}, Option {option}", word, option);

            foreach (var exerciseWord in data.Words)
            {
                if (exerciseWord.Word == word)
                {
                    foreach (var exerciseOption in exerciseWord.Options)
                    {
                        exerciseOption.IsSelected = exerciseOption.Option == option;
                    }
                    break;
                }
            }

            ModelState.Clear();

            return View("Exercise", new ExerciseModel() { Data = data });
        }


        private async Task<ExerciseModel> GetExerciseModel(int exerciseId)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/ExerciseApi/{exerciseId}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            var exerciseVocabularyData = JsonSerializer.Deserialize<ExerciseVocabularyData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var exerciseModel = ModelFactory.CreateExerciseModel(exerciseVocabularyData);

            return exerciseModel;
        }
    }
}
