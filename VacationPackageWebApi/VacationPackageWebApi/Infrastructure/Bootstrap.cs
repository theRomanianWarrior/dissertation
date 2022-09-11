using System.Text.Json;
using ActressMas;
using VacationPackageWebApi.Application.Services;
using VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Attractions.Contracts;
using VacationPackageWebApi.Domain.Customer.Contracts;
using VacationPackageWebApi.Domain.Flight.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.Property.Contracts;
using VacationPackageWebApi.Domain.Services;
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
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IPreferencesPackageService, PreferencesPackageService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IAttractionService, AttractionService>();
            services.AddScoped<IMasLoaderService, MasLoaderService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
            services.AddScoped<IPreferencesPayloadInitializerServices, PreferencesPayloadInitializerServices>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IPreferencesPackageRequestRepository, PreferencesPackageRequestRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IAttractionRepository, AttractionsRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddRouting(options => options.LowercaseUrls = true);

            AddDb(services, configuration);

            LoadMassEnvironment(services);
        }

        private static void LoadMassEnvironment(IServiceCollection services)
        {
            var masLoaderService = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IMasLoaderService>();
            masLoaderService.LoadMasEnvironmentAsync();
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
