using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Threading.Tasks;

using System.IO;
using BibleWebApi;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordQuiz.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LexemeData? LexemeData { get; set; }

        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7085/")
        };

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/lexeme/1");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                LexemeData = JsonSerializer.Deserialize<LexemeData>(json, options);
            }
        }

        private async Task<LexemeData?> GetLexemeData()
        {
            LexemeData? lexeme = null;
            HttpResponseMessage response = await client.GetAsync("~/Lexeme/1");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                lexeme= JsonSerializer.Deserialize<LexemeData>(json);

            }
            return lexeme;
        }
    }
}
