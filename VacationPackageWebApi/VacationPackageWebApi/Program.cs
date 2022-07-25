using ActressMas;
using VacationPackageWebApi.API.Infrastructure;

var builder = Bootstrap.CreateAppBuilder(args);

var masEnv = new EnvironmentMas();

var app = Bootstrap.BuildApp(builder, masEnv);

app.Run();