using System;

namespace DbPopulator.Models
{
    public record FlightConnection
    {
        public Guid Id { get; set; }
        public Guid FlightRecommandationId { get; set; }
        public Guid FlightId { get; set; }
    }
}
