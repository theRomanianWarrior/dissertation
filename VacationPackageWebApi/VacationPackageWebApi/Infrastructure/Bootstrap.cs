using System.Text.Json;
using VacationPackageWebApi.Application.Services;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories;
using VacationPackageWebApi.Infrastructure.Repositories.Repositories;

namespace VacationPackageWebApi.API.Infrastructure
{
    public static class Bootstrap
    {
        public static string AppSettingsPath { get; set; } = "appsettings.json";

        public static string EnvFilename { get; set; } = ".env";


        public static WebApplicationBuilder CreateAppBuilder(string[] args)
            => WebApplication.CreateBuilder(args);

        public static WebApplication BuildApp(WebApplicationBuilder builder)
        {
            var configuration = GetConfiguration();

            builder.WebHost
                .UseConfiguration(configuration)
                .UseUrls(configuration.GetValue<string>("Hostings:Urls"));

            ConfigureServices(builder.Services, configuration);

            var app = builder.Build();

            ConfigureApp(app);

            return app;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddScoped<PreferencesPackageService>();
            services.AddScoped<IPreferencesPackageRequestRepository, PreferencesPackageRequestRepository>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddRouting(options => options.LowercaseUrls = true);

            AddDb(services, configuration);

        }

        private static void AddDb(IServiceCollection services, IConfiguration configuration)
        {
            var vacationPackageDatabaseOptions = configuration.GetOptions<VacationPackageDatabaseOptions>(VacationPackageDatabaseOptions.ConfigKey);
            services.AddEntityFramework(vacationPackageDatabaseOptions);
        }

        /// Configures the HTTP request pipeline.
        private static void ConfigureApp(WebApplication app)
        {
            app.HandleRouteNotFound();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsPath, false, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsPath, false, false);

            builder.AddEnvironmentVariables();
        }

        private static void HandleRouteNotFound(this WebApplication app)
        {
            app.UseStatusCodePages(new StatusCodePagesOptions
            {
                HandleAsync = async ctx =>
                {
                    if (ctx.HttpContext.Response.StatusCode == 404)
                    {
                        var result = JsonSerializer.Serialize(new
                        {
                            message = "Route not found"
                        });

                        ctx.HttpContext.Response.ContentType = "application/json";

                        await ctx.HttpContext.Response.WriteAsync(result);
                    }
                }
            });
        }
    }
}
