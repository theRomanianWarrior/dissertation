using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightMapper
{
    public static FlightBusinessModel ToBusinessModel(this Flight flight, List<FlightPriceBusinessModel> flightPriceList)
    {
        return new FlightBusinessModel
        {
            Id = flight.Id,
            ArrivalAirport = flight.ArrivalAirport.ToBusinessModel(),
            AvailableDepartureTime = flight.AvailableDepartureTime.ToBusinessModel(),
            Company = flight.Company.ToBusinessModel(),
            DepartureAirport = flight.DepartureAirport.ToBusinessModel(),
            Duration = flight.Duration,
            WeekDaysOfFlight = flight.WeekDaysOfFlight.ToBusinessModel(),
            FlightPriceList = flightPriceList,
        };
    }
}