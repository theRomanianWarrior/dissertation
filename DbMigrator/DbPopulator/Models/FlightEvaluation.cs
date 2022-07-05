using System;

namespace DbPopulator.Models
{
    public record FlightEvaluation
    {
        public Guid Id { get; set; }
        public short Class { get; set; }
        public short TypeOfFlight { get; set; }
        public short Stops { get; set; }
        public short Connections { get; set; }
        public short FinalFlightRating { get; set; }
    }
}
