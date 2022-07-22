using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class AirportMapper
{
    public static AirportBusinessModel ToBusinessModel(this Airport airport)
    {
        return new AirportBusinessModel()
        {
            Id = airport.Id,
            City = airport.City.ToBusinessModel(),
            Name = airport.Name
        };
    }
}