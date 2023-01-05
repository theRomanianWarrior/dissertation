using DbMigrator.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DbMigrator
{
    public static class EntityFrameworkRegistration
    {
        public static void AddEntityFramework(this IServiceCollection services, VacationPackageDatabaseOptions config)
        {
            services.AddDbContext<VacationPackageContext>(dbContextBuilder =>
            {
                dbContextBuilder
                    .UseNpgsql(config.ConnectionString);
            });
        }
    }

}
