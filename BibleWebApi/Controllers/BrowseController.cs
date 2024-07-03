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
        public async Task<ActionResult> Index(string? id)
        {
            id ??= "John 3:16";

            var model = await RenderRage(id);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(BrowseModel model)
        {
            var newModel = await RenderRage(model.RangeExpression);

            return View("Index", newModel);
        }

        private async Task<BrowseModel> RenderRage(string? range)
        {
            var request = HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/TextApi?range={range}";

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            var textData = JsonSerializer.Deserialize<TextData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");

            var rangeExpression = textData.RangeExpression;
            var message = string.Empty;
            //if (rangeExpression != null)
            //{
            //    rangeExpression = textData?.RangeExpression;
            //}
            //else
            //{
            //    message = "Range not recognized.";
            //}

            return new BrowseModel()
            {
                TextData = textData,
                RangeExpression = rangeExpression,
                Message = message
            };
        }
    }
}
