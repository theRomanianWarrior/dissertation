using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public DateOnly FlightDate { get; set; }
        public short Stops { get; set; }
    }
}
