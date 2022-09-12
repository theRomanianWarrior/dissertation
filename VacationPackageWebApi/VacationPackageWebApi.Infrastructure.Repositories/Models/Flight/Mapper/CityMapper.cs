using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class CityMapper
{
    public static CityBusinessModel ToBusinessModel(this City city)
    {
        return new CityBusinessModel
        {
            Id = city.Id,
            Country = city.Country.ToBusinessModel(),
            Name = city.Name
        };
    }
}