using FluentMigrator.Runner;
using Npgsql;

namespace DbPopulator
{
    public static class DatabaseHandler
    {
        public static async Task ConnectAsync(string migrationConnectionString, string catalogName)
        {
            await using var connection = new NpgsqlConnection(migrationConnectionString);
            await connection.OpenAsync();

            if (await DatabaseDoesNotExistAsync(catalogName, connection))
            {
                Console.WriteLine("Database does not exist.");
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
