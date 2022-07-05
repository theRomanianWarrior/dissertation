using System;

namespace DbPopulator.Models
{
    public record FlightPrice
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public short ClassId { get; set; }
        public short Price { get; set; }
    }
}
