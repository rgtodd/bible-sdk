using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WordQuiz.Models;

namespace WordQuiz.Data
{
    public class WordQuizContext : DbContext
    {
        public WordQuizContext (DbContextOptions<WordQuizContext> options)
            : base(options)
        {
        }

        public DbSet<WordQuiz.Models.Movie> Movie { get; set; } = default!;
    }
}
