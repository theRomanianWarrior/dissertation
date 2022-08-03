using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
        public Guid InitialAssignedAgentId { get; set; }
        public DateOnly FlightDate { get; set; }
        public short Stops { get; set; }
        public TimeOnly Time { get; set; }
    }
}
