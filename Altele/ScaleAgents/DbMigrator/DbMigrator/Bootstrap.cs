using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DbMigrator
{
    public static class Bootstrap
    {
        public static string AppSettingsPath { get; set; } = "appsettings.json";

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

            return app;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            AddDb(services, configuration);
        }

        private static void AddDb(IServiceCollection services, IConfiguration configuration)
        {
            var vacationPackageDatabaseOptions = configuration.GetOptions<VacationPackageDatabaseOptions>(VacationPackageDatabaseOptions.ConfigKey);
            services.AddEntityFramework(vacationPackageDatabaseOptions);
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsPath, false, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
