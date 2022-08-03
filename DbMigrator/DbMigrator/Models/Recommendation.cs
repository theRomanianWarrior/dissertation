using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class Recommendation
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid PropertyRecommendationId { get; set; }
        public Guid AttractionRecommendationId { get; set; }
    }
}
