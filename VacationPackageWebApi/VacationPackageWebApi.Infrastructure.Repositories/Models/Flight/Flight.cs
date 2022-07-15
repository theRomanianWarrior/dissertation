using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record Flight
    {
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
    }
}
