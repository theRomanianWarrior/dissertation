using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Flight.Contracts;

namespace VacationPackageWebApi.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<List<FlightBusinessModel>> GetAllFlightsAsync()
    {
        return await _flightRepository.GetAllFlights();
    }

    public List<string> GetFlightDepartureCities()
    {
        var flightsDepartureDestinationCities = _flightRepository.GetFlightsByUniqueDepartureArrivalAirport();

        return flightsDepartureDestinationCities.Select(flightDepartureDestination =>
                flightDepartureDestination.DepartureCityName + ", " + flightDepartureDestination.DepartureCountryName)
            .Distinct().ToList();
    }

    public List<string> GetFlightArrivalCities(string flightDepartureCity)
    {
        var flightArrivalCities = _flightRepository.GetFlightArrivalCities(flightDepartureCity);

        /* Destinations black list
         * Athens
         * Kazan
         * Nikolayev
         * Sabetta
         This is a temporary implementation. */
        foreach (var city in flightArrivalCities.ToList())
            switch (city.DestinationCityName)
            {
                case "Athens":
                case "Kazan":
                case "Nikolayev":
                case "Sabetta":
                    flightArrivalCities.Remove(city);
                    break;
            }

        return flightArrivalCities.Select(flightArrivalDestination => flightArrivalDestination.DestinationCityName +
                                                                      ", " + flightArrivalDestination
                                                                          .DestinationCountryName).Distinct().ToList();
    }

    public List<string> GetFlightCompaniesForDepartureDestinationCity(string departureAndDestinationCity)
    {
        var departureDestination = departureAndDestinationCity.Split(", ");
        var departureCity = departureDestination[0];
        var destinationCity = departureDestination[1];

        return _flightRepository.GetFlightCompaniesForCities(departureCity, destinationCity);
    }
}