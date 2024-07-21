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
        public async Task<IActionResult> Index(string? wordListId, string? range)
        {
            var model = await GetExerciseCatalogModel(wordListId, range);

            return View("Catalog", model);
        }

        [HttpPost]
        public async Task<IActionResult> Begin(ExerciseCatalogModel model, string factory)
        {
            try
            {
                var exerciseModel = await GetExerciseModel(factory, model.WordListId, model.Range, false);
                return View("Exercise", exerciseModel);
            }
            catch (ApplicationException ex)
            {
                ModelState.Clear();

                model = await GetExerciseCatalogModel(model.WordListId, model.Range);
                model.Message = ex.Message;
                return View("Catalog", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Start(string name, string? wordListId, string? range)
        {
            var model = await GetExerciseModel(name, wordListId, range, false);

            return View("Exercise", model);
        }

        [HttpGet]
        public async Task<IActionResult> Study(string name, string? wordListId, string? range)
        {
            var model = await GetExerciseModel(name, wordListId, range, true);

            return View("Study", model);
        }

        [HttpPost]
        public IActionResult Update(ExerciseModel model, string? question, string? answer)
        {
            ModelState.Clear();

            var questions = ExerciseModel.RestoreQuestionsMomento(model.QuestionsMomento);

            Logger.LogInformation("Word {word}, Option {option}", question, answer);

            foreach (var exerciseQuestion in questions)
            {
                if (exerciseQuestion.Question == question)
                {
                    foreach (var exerciseAnswer in exerciseQuestion.Answers)
                    {
                        exerciseAnswer.IsSelected = exerciseAnswer.Answer == answer;
                    }
                    break;
                }
            }

            return View("Exercise", new ExerciseModel()
            {
                Name = model.Name,
                WordListDescription = model.WordListDescription,
                WordListId = model.WordListId,
                Range = model.Range,
                Questions = questions,
                QuestionsMomento = ExerciseModel.CreateQuestionsMomento(questions)
            });
        }

        private async Task<ExerciseCatalogModel> GetExerciseCatalogModel(string? wordListId, string? range)
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

            var exerciseCatalogModel = ModelFactory.CreateExerciseCatalogModel(exerciseCatalogData, wordListId, range);

            return exerciseCatalogModel;
        }

        private async Task<ExerciseModel> GetExerciseModel(string name, string? wordListId, string? range, bool sort)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/ExerciseApi/exercise?name={name}&wordListId={wordListId}&range={range}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");
                throw new ApplicationException(message);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            var exerciseData = JsonSerializer.Deserialize<ExerciseData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var exerciseModel = ModelFactory.CreateExerciseModel(exerciseData, sort);

            return exerciseModel;
        }
    }
}
