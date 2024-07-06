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
            var model = await GetExerciseCatalogModel();

            return View("Catalog", model);
        }

        [HttpGet]
        public async Task<IActionResult> Start(string categoryName, string name)
        {
            var model = await GetExerciseModel(categoryName, name, false);

            return View("Exercise", model);
        }

        [HttpGet]
        public async Task<IActionResult> Study(string categoryName, string name)
        {
            var model = await GetExerciseModel(categoryName, name, true);

            return View("Study", model);
        }

        [HttpPost]
        public IActionResult Update(ExerciseDataModel data, string? word, string? option)
        {
            ModelState.Clear();

            Logger.LogInformation("Word {word}, Option {option}", word, option);

            foreach (var question in data.Questions)
            {
                if (question.Question == word)
                {
                    foreach (var answer in question.Answers)
                    {
                        answer.IsSelected = answer.Answer == option;
                    }
                    break;
                }
            }

            return View("Exercise", new ExerciseModel() { Data = data });
        }


        private async Task<ExerciseCatalogModel> GetExerciseCatalogModel()
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/ExerciseApi/catalog";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            var exerciseCatalogData = JsonSerializer.Deserialize<ExerciseCatalogData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var exerciseCatalogModel = ModelFactory.CreateExerciseCatalogModel(exerciseCatalogData);

            return exerciseCatalogModel;
        }

        private async Task<ExerciseModel> GetExerciseModel(string categoryName, string name, bool sort)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/ExerciseApi/exercise?categoryName={categoryName}&name={name}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            var exerciseVocabularyData = JsonSerializer.Deserialize<ExerciseData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var exerciseModel = ModelFactory.CreateExerciseModel(exerciseVocabularyData, sort);

            return exerciseModel;
        }
    }
}
