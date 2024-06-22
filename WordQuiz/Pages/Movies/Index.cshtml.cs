using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WordQuiz.Data;
using WordQuiz.Models;

namespace WordQuiz.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly WordQuiz.Data.WordQuizContext _context;

        public IndexModel(WordQuiz.Data.WordQuizContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
