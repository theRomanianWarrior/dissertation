namespace VacationPackageWebApi.Domain.Flight.Contracts;

public interface IFlightRepository
{
    public Task<List<FlightBusinessModel>> GetAllFlights();
}