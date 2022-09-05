using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Flight.Contracts;
using VacationPackageWebApi.Domain.Flight.UIModels;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly VacationPackageContext _context;

    public FlightRepository(VacationPackageContext context)
    {
        _context = context;
    }

    public async Task<List<FlightBusinessModel>> GetAllFlights()
    {
        var flights = await _context.Flights.Include(a => a.ArrivalAirport)
            .Include(d => d.DepartureAirport).ThenInclude(ct => ct.City).ThenInclude(ctr => ctr.Country)
            .Include(c => c.Company)
            .Include(w => w.WeekDaysOfFlight)
            .Include(av => av.AvailableDepartureTime).ToListAsync();
        
        var prices = await _context.FlightPrices.Include(c => c.Class).ToListAsync();
        
       return 
            (from flight in flights
                join flightPrice in prices on flight.Id equals flightPrice.FlightId into pricesOfFlight
                select flight.ToBusinessModel(pricesOfFlight.Select(fp => fp.ToBusinessModel()).ToList())).ToList();
    }

    public List<FlightDepartureCities> GetFlightsByUniqueDepartureArrivalAirport()
    {
        return _context.Flights.Include(fd => fd.DepartureAirport).ThenInclude(fdc => fdc.City).ThenInclude(fdcc => fdcc.Country)
            .Select(f => f.ToUiDepartureCitiesModel()).ToList().OrderBy(c => c.DepartureCountryName).ToList();
    }

    public List<FlightDestinationCities> GetFlightArrivalCities(string flightDepartureCity)
    {
        return _context.Flights.Include(f => f.DepartureAirport).ThenInclude(fd => fd.City)
            .Include(fr => fr.ArrivalAirport).ThenInclude(fda => fda.City).ThenInclude(fdar => fdar.Country)
            .Where(f => f.DepartureAirport.City.Name.Contains(flightDepartureCity))
            .Select(ac => ac.ToUiDestinationCitiesModel()).ToList().OrderBy(c => c.DestinationCountryName).ToList();
    }
}