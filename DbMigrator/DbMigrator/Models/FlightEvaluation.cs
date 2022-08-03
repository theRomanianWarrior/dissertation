using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightEvaluation
    {
        public Guid Id { get; set; }
        public short Class { get; set; }
        public short Price { get; set; }
        public short Company { get; set; }
        public short FinalFlightRating { get; set; }
    }
}
