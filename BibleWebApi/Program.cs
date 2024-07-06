using BibleCore.Service;

using System.Text.Json.Serialization;

namespace BibleWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ASP.NET MVC Support
            //
            builder.Services.AddControllersWithViews();

            // Add IHttpClientFactory
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0
            //
            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure JSON serialization.
            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2293
            //
            builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // DI for application services.
            //
            BibleServiceRegistrar.Register(builder.Services);
            //builder.Services.AddSingleton<IGlobalGreek, GlobalGreek>();
            //builder.Services.AddScoped<ILexemeService, LexemeService>();
            //builder.Services.AddScoped<ITextService, TextService>();
            //builder.Services.AddScoped<IExerciseService, ExerciseService>();

            var app = builder.Build();

            // Enable Swagger in development
            //
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTP to HTTPS redirection.
            //
            app.UseHttpsRedirection();

            // Enable routing middleware required by ASP.NET MVC
            //
            app.UseRouting();

            app.UseAuthorization();

            // Configure default routing used by ASP.NET MVC controllers.
            //
            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Serve content under wwwroot
            //
            app.UseStaticFiles();

            app.Run();
        }
    }
}
