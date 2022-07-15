using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories
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
