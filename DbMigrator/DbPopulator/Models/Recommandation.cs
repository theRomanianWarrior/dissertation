using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record Recommandation
    {
        public Guid Id { get; set; }
        public Guid FlightRecommandationId { get; set; }
        public Guid PropertyRecommandationId { get; set; }
        public Guid AttractionRecommandationId { get; set; }
    }
}
