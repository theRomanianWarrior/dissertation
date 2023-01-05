using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebAPI.Flight.Contracts;

public interface IFlightRepository
{
    public Task<List<FlightBusinessModel>> GetAllFlights();
}