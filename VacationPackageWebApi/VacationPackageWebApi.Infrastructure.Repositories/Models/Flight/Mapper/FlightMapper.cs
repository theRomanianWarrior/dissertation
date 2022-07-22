using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightMapper
{
    public static FlightBusinessModel ToBusinessModel(this Flight flight, List<FlightPriceBusinessModel> flightPriceList, List<Guid> agentsIdList)
    {
        var random = new Random();
        var randomAgentId = random.Next(agentsIdList.Count);
        
        return new FlightBusinessModel
        {
            Id = flight.Id,
            ArrivalAirport = flight.ArrivalAirport.ToBusinessModel(),
            AvailableDepartureTime = flight.AvailableDepartureTime.ToBusinessModel(),
            Company = flight.Company.ToBusinessModel(),
            DepartureAirport = flight.DepartureAirport.ToBusinessModel(),
            Duration = flight.Duration,
            WeekDaysOfFlight = flight.WeekDaysOfFlight.ToBusinessModel(),
            FlightPriceList = flightPriceList,
            StoredInLocalDbOfAgentWithId = agentsIdList[randomAgentId]
        };
    }
}