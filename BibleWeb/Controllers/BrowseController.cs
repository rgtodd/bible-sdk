using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWebApi.Controllers
{
    public class BrowseController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index(string? range)
        {
            range ??= "John 3:16";

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi?range={range}";

            var newModel = await GetBrowseModel(range, url);

            return View("Index", newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrowseModel model)
        {
            ModelState.Clear();

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi?range={model.RangeExpression}";

            model = await GetBrowseModel(model.RangeExpression, url);

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Practice(BrowseModel model)
        {
            return RedirectToAction("Index", "Exercise", new { range = model.RangeExpression });
        }

        [HttpPost]
        public async Task<IActionResult> MovePrevious(BrowseModel model)
        {
            ModelState.Clear();

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi/movePrevious?range={model.RangeExpression}";

            model = await GetBrowseModel(model.RangeExpression, url);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> MoveNext(BrowseModel model)
        {
            ModelState.Clear();

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi/moveNext?range={model.RangeExpression}";

            model = await GetBrowseModel(model.RangeExpression, url);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> ExtendPrevious(BrowseModel model)
        {
            ModelState.Clear();

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi/extendPrevious?range={model.RangeExpression}";

            model = await GetBrowseModel(model.RangeExpression, url);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> ExtendNext(BrowseModel model)
        {
            ModelState.Clear();

            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi/extendNext?range={model.RangeExpression}";

            model = await GetBrowseModel(model.RangeExpression, url);

            return View("Index", model);
        }

        private async Task<BrowseModel> GetBrowseModel(string? range, string url)
        {
            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new BrowseModel()
                {
                    RangeExpression = range,
                    Message = "Not found."
                };
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");
            if (string.IsNullOrEmpty(json))
            {
                return new BrowseModel()
                {
                    RangeExpression = range,
                    Message = "Not found."
                };
            }

            var textData = JsonSerializer.Deserialize<TextData?>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var rangeExpression = textData.RangeExpression;
            var message = string.Empty;

            return new BrowseModel()
            {
                TextData = textData,
                RangeExpression = rangeExpression,
                Message = message
            };
        }
    }
}
