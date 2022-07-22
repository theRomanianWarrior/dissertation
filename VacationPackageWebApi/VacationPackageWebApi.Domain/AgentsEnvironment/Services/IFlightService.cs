using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IFlightService
{
    public Task<List<FlightBusinessModel>> GetAllFlightsAsync();
}