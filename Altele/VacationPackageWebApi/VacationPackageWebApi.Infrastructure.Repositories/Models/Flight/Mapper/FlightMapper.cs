using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Flight.UIModels;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightMapper
{
    public static FlightBusinessModel ToBusinessModel(this Flight flight,
        List<FlightPriceBusinessModel> flightPriceList)
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
            FlightPriceList = flightPriceList
        };
    }

    public static FlightDepartureCities ToUiDepartureCitiesModel(this Flight flight)
    {
        return new FlightDepartureCities
        {
            DepartureCityName = flight.DepartureAirport.City.Name,
            DepartureCountryName = flight.DepartureAirport.City.Country.Name
        };
    }

    public static FlightDestinationCities ToUiDestinationCitiesModel(this Flight flight)
    {
        return new FlightDestinationCities
        {
            DestinationCityName = flight.ArrivalAirport.City.Name,
            DestinationCountryName = flight.ArrivalAirport.City.Country.Name
        };
    }
}