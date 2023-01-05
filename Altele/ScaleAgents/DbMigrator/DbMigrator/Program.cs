using DbMigrator;
var builder = Bootstrap.CreateAppBuilder(args);

var app = Bootstrap.BuildApp(builder);

var sqlConfig = builder.Configuration.GetOptions<VacationPackageDatabaseOptions>(VacationPackageDatabaseOptions.ConfigKey);
await DatabaseMigrator.Update(sqlConfig);

app.Run();
