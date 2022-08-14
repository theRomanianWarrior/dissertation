using System;


namespace DbMigrator.Models
{
    public class Flight
    {
        public Guid Id { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public short Duration { get; set; }
        public Guid CompanyId { get; set; }
        public Guid WeekDaysOfFlightId { get; set; }
        public Guid AvailableDepartureTimeId { get; set; }
    }
}
