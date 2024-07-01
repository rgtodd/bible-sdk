using BibleCore.Service;
using System.Text.Json.Serialization;

namespace BibleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0
            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2293
            builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            //WEB
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<IGlobalGreek, GlobalGreek>();
            builder.Services.AddScoped<ILexemeService, LexemeService>();
            builder.Services.AddScoped<ITextService, TextService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // WEB
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            // WEB 
            app.MapRazorPages();

            app.Run();
        }
    }
}
