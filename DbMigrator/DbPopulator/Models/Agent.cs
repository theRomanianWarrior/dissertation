using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record Agent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float FlightSelfExpertRate { get; set; }
        public float PropertySelfExpertRate { get; set; }
        public float AttractionsSelfExpertRate { get; set; }
    }
}
