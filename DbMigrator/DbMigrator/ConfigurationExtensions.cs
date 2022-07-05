using Microsoft.Extensions.Configuration;
namespace DbMigrator
{
    public static class ConfigurationExtensions
    {
        public static T GetOptions<T>(this IConfiguration configuration, string configKey)
    where T : class, new()
        {
            var options = new T();
            configuration.GetSection(configKey).Bind(options);
           
            return options;
        }
    }
}
