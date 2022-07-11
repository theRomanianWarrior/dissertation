using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record Recommendation
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid PropertyRecommendationId { get; set; }
        public Guid AttractionRecommendationId { get; set; }
    }
}
