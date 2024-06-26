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

namespace WordQuiz.Pages
{
    public class IndexModel(ILogger<IndexModel> logger) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;

        public required LexemeData LexemeData { get; set; } = LexemeData.Empty;

        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7085/")
        };

        [BindProperty]
        public string Strongs { get; set; }

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/lexeme/1");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                LexemeData = JsonSerializer.Deserialize<LexemeData>(json, PageResources.JsonSerializerOptions) ?? LexemeData.Empty;
            }
        }

        public async  Task OnPostAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/lexeme/" + Strongs);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                LexemeData = JsonSerializer.Deserialize<LexemeData>(json, PageResources.JsonSerializerOptions) ?? LexemeData.Empty;
            }
        }
    }
}
