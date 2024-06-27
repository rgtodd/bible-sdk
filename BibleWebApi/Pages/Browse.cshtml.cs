using BibleCore.Service.Data;

using BibleWebApi.Code;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http;

using System.Text.Json;

namespace WordQuiz.Pages
{
    public class BrowseModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        private IHttpClientFactory HttpClientFactory { get; init; } = httpClientFactory;

        public TextData? TextData { get; set; }

        public async Task OnGet()
        {
            var request = PageContext.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/text";

            var c = HttpClientFactory.CreateClient();
            HttpResponseMessage response = await c.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                TextData = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<TextData>(json, PageResources.JsonSerializerOptions);
            }


        }
    }
}
