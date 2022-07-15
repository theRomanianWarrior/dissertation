namespace VacationPackageWebApi.Infrastructure.Repositories
{
    public record VacationPackageDatabaseOptions
    {
        public const string ConfigKey = "VacationPackageDatabase";

        public string ConnectionString { get; init; } = null!;
        public string MigrationConnectionString { get; init; } = null!;
        public string InitialCatalog { get; init; } = null!;
    }
}
