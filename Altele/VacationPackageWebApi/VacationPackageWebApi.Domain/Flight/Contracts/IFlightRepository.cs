using VacationPackageWebApi.Domain.Flight.UIModels;

namespace VacationPackageWebApi.Domain.Flight.Contracts;

public interface IFlightRepository
{
    public Task<List<FlightBusinessModel>> GetAllFlights();
    public List<FlightDepartureCities> GetFlightsByUniqueDepartureArrivalAirport();
    public List<FlightDestinationCities> GetFlightArrivalCities(string flightDepartureCity);
    List<string> GetFlightCompaniesForCities(string departureCity, string destinationCity);
}