using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleCore.Service
{
    public static class BibleServiceRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IGlobalGreek, GlobalGreek>();
            services.AddScoped<ILexemeService, LexemeService>();
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<IExerciseService, ExerciseService>();
        }
    }
}
