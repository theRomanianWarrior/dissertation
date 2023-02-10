using FluentMigrator.Runner;
using Npgsql;
using System.Threading.Tasks;

namespace DbMigrator
{
    public static class DatabaseHandler
    {
        public static async Task CreateAsync(string migrationConnectionString, string catalogName)
        {
            await using var connection = new NpgsqlConnection(migrationConnectionString);
            await connection.OpenAsync();

            if (await DatabaseDoesNotExistAsync(catalogName, connection))
            {
                await using var cmd = new NpgsqlCommand($"create database {catalogName}", connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static void Update(IMigrationRunner runner, bool rollback = false)
        {
            runner.ListMigrations();

            if (rollback && runner.HasMigrationsToApplyRollback())
            {
                runner.Rollback(1);
                return;
            }

            runner.MigrateUp();
        }

        private static async Task<bool> DatabaseDoesNotExistAsync(string dbName, NpgsqlConnection connection)
        {
            await using var command = new NpgsqlCommand(
                $"select COUNT(1) from pg_catalog.pg_database where datname = '{dbName}';",
                connection);

            return (long)command.ExecuteScalar()! == 0;
        }
    }
}
