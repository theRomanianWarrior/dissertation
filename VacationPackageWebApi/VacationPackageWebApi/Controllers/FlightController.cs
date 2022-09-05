using Microsoft.AspNetCore.Mvc;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;

namespace VacationPackageWebApi.API.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class FlightController : Controller
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    
    [HttpGet]
    public List<string> GetFlightDepartureCities()
    {
        return _flightService.GetFlightDepartureCities();
    }
    

    [HttpGet("{flightDepartureCity}")]
    public List<string> GetFlightArrivalCities(string flightDepartureCity)
    {
        return _flightService.GetFlightArrivalCities(flightDepartureCity);
    }
}