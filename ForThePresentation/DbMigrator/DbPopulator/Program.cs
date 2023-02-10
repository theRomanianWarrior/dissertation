using DbMigrator;

var builder = Bootstrap.CreateAppBuilder(args);

var sqlConfig = builder.Configuration.GetOptions<DbMigrator.VacationPackageDatabaseOptions>(DbMigrator.VacationPackageDatabaseOptions.ConfigKey);

await DbPopulator.DatabaseMigrator.Update(sqlConfig);