using VacationPackageWebApi.API.Infrastructure;
using VacationPackageWebApi.Infrastructure.Repositories;

var builder = Bootstrap.CreateAppBuilder(args);

var app = Bootstrap.BuildApp(builder);

app.Run();
