using BibleCore.Service.Data;
using BibleCore.Utility;

using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BibleWebApi.Controllers
{
    public class LookupController(IHttpClientFactory httpClientFactory) : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        [HttpGet]
        public async Task<IActionResult> Index(int? strongs, int? gk)
        {
            var model = new LookupModel();

            if (strongs != null || gk != null)
            {
                var request = HttpContext.Request;
                string url;
                if (strongs != null)
                {
                    url = $"{request.Scheme}://{request.Host}/api/LexemeApi/strongs/{strongs}";
                }
                else // Gk must be specified.
                {
                    url = $"{request.Scheme}://{request.Host}/api/LexemeApi/gk/{gk}";
                }

                var c = HttpClientFactory.CreateClient();
                var response = await c.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(response.ReasonPhrase);
                }

                var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

                model.LexemeData = JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");
                if (model.LexemeData != null)
                {
                    if (model.LexemeData.StrongsNumber.Length > 0)
                    {
                        model.StrongsNumber = model.LexemeData.StrongsNumber[0];
                    }
                    if (model.LexemeData.GkNumber.Length > 0)
                    {
                        model.GkNumber = model.LexemeData.GkNumber[0];
                    }
                }
                else
                {
                    model.Message = "Not found.";
                }
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LookupModel model)
        {
            var strongsNumber = model.StrongsNumber;
            var gkNumber = model.GkNumber;

            ModelState.Clear();

            model = new LookupModel
            {
                StrongsNumber = strongsNumber,
                GkNumber = gkNumber
            };

            var request = HttpContext.Request;
            string url;
            if (strongsNumber != null)
            {
                url = $"{request.Scheme}://{request.Host}/api/LexemeApi/strongs/{strongsNumber}";
            }
            else if (gkNumber != null)
            {
                url = $"{request.Scheme}://{request.Host}/api/LexemeApi/gk/{gkNumber}";
            }
            else
            {
                return View("Index", model);
            }

            var c = HttpClientFactory.CreateClient();
            var response = await c.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                model.Message = "Not found.";
                return View("Index", model);
            }

            var json = await response.Content.ReadAsStringAsync() ?? throw new ApplicationException("Empty response");

            model.LexemeData = JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions) ?? throw new ApplicationException("Null deserialization.");
            if (model.LexemeData != null)
            {
                if (model.LexemeData.StrongsNumber.Length > 0)
                {
                    model.StrongsNumber = model.LexemeData.StrongsNumber[0];
                }
                if (model.LexemeData.GkNumber.Length > 0)
                {
                    model.GkNumber = model.LexemeData.GkNumber[0];
                }
            }
            else
            {
                model.Message = "Not found.";
            }

            return View("Index", model);
        }
    }
}
