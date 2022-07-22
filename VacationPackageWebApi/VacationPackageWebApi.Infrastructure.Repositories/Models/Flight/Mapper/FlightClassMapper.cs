using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightClassMapper
{
    public static FlightClassBusinessModel ToBusinessModel(this FlightClass flightClass)
    {
        return new FlightClassBusinessModel()
        {
            Id = flightClass.Id,
            Class = flightClass.Class
        };
    }
}