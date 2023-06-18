using NJsonSchema.Generation.TypeMappers;

namespace VacationPackageWebApi.API.Infrastructure.OpenApi;

internal static class Startup
{
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

        if (settings is {Enable: true})
        {
            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument((document, serviceProvider) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = settings.Title;
                    doc.Info.Version = settings.Version;
                    doc.Info.Description = settings.Description;
                };
                
                document.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TimeSpan), schema =>
                {
                    schema.Type = NJsonSchema.JsonObjectType.String;
                    schema.IsNullableRaw = true;
                    schema.Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
                    schema.Example = "02:00:00";
                }));

                document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());

            });

        }
        return services;
    }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            if (config.GetValue<bool>("SwaggerSettings:Enable"))
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(options =>
                {
                    options.DefaultModelsExpandDepth = -1;
                    options.DocExpansion = "none";
                    options.TagsSorter = "alpha";
                });
            }

            return app;
        }

}