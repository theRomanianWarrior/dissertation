using System;

namespace DbPopulator.Models
{
    public record FlightWeekItinerary
    {
        public Guid Id { get; set; }
        public short DayOfWeekId { get; set; }
        public Guid FlightId { get; set; }
        public short DepartureTime { get; set; }
    }
}
