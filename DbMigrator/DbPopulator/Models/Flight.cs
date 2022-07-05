using System;

namespace DbPopulator.Models
{
    public record Flight
    {
        public Guid Id { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public short Duration { get; set; }
        public Guid CompanyId { get; set; }
    }
}
