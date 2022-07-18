using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class Flight
    {
        public Flight()
        {
            FlightConnections = new HashSet<FlightConnection>();
            FlightPrices = new HashSet<FlightPrice>();
        }

        public Guid Id { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public short Duration { get; set; }
        public Guid CompanyId { get; set; }
        public Guid WeekDaysOfFlightId { get; set; }
        public Guid AvailableDepartureTimeId { get; set; }

        public virtual Airport ArrivalAirport { get; set; }
        public virtual AvailableDepartureTime AvailableDepartureTime { get; set; }
        public virtual FlightCompany Company { get; set; }
        public virtual Airport DepartureAirport { get; set; }
        public virtual WeekDaysOfFlight WeekDaysOfFlight { get; set; }
        public virtual ICollection<FlightConnection> FlightConnections { get; set; }
        public virtual ICollection<FlightPrice> FlightPrices { get; set; }
    }
}
