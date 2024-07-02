using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Threading.Tasks;

using System.IO;
using BibleWebApi;
using System.Text.Json;
using System.Text.Json.Serialization;
using BibleWebApi.Code;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using BibleCore.Utility;

namespace WordQuiz.Pages
{
    public class LookupModel(ILogger<LookupModel> logger, IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly ILogger<LookupModel> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public string? Message { get; set; }

        public LexemeData? LexemeData { get; set; }

        [BindProperty]
        public string? Strongs { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id == null)
            {
                LexemeData = null;
                Message = null;
            }
            else
            {
                var request = PageContext.HttpContext.Request;
                var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/{id.Value}";

                var c = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await c.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    LexemeData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions);
                }

                Message = LexemeData == null ? "Not found." : null;
                Strongs = id.Value.ToString();
            }
        }

        public async Task OnPostAsync()
        {
            var request = PageContext.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/LexemeApi/{Strongs}";

            var c = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await c.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                LexemeData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<LexemeData>(json, Serialization.JsonSerializerOptions);
            }

            Message = LexemeData == null ? "Not found." : null;
        }
    }
}
