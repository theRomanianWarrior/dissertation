using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightPriceMapper
{
    public static FlightPriceBusinessModel ToBusinessModel(this FlightPrice flightPrice)
    {
        return new FlightPriceBusinessModel
        {
            Id = flightPrice.Id,
            Class = flightPrice.Class.ToBusinessModel(),
            //Flight = flightPrice.Flight.ToBusinessModel(null)
            Price = flightPrice.Price
        };
    }
}