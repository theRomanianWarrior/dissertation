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

        return flightsDepartureDestinationCities.Select(flightDepartureDestination => flightDepartureDestination.DepartureCityName + ", " + flightDepartureDestination.DepartureCountryName).Distinct().ToList();
    }

    public List<string> GetFlightArrivalCities(string flightDepartureCity)
    {
        var flightArrivalCities =  _flightRepository.GetFlightArrivalCities(flightDepartureCity);
        
        return flightArrivalCities.Select(flightArrivalDestination => flightArrivalDestination.DestinationCityName + ", " + flightArrivalDestination.DestinationCountryName).Distinct().ToList();
    }
}