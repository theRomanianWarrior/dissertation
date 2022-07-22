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
}