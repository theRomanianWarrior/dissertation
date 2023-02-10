using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Management.Automation;
using System.Reflection;
using System.Threading.Tasks;

namespace DbMigrator
{
    public static class DatabaseMigrator
    {
        public static async Task Update(VacationPackageDatabaseOptions sqlConfig, bool rollback = false)
        {
            Console.WriteLine("VacationPackage.DbMigrator App has STARTED");

            await DatabaseHandler.CreateAsync(sqlConfig.MigrationConnectionString, sqlConfig.InitialCatalog);

            var serviceProvider = CreateServices(sqlConfig.ConnectionString);

            using var scope = serviceProvider.CreateScope();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            DatabaseHandler.Update(runner, rollback);

            Console.WriteLine("VacationPackage.DbMigrator App work done");
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres11_0()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
