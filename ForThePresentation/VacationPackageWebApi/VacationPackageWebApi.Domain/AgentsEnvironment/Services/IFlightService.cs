using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IFlightService
{
    public Task<List<FlightBusinessModel>> GetAllFlightsAsync();
    public List<string> GetFlightDepartureCities();
    List<string> GetFlightArrivalCities(string flightDepartureCity);
    List<string> GetFlightCompaniesForDepartureDestinationCity(string departureAndDestinationCity);
}