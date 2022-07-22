using VacationPackageWebApi.API.Infrastructure;

var builder = Bootstrap.CreateAppBuilder(args);

var app = Bootstrap.BuildApp(builder);

app.Run();