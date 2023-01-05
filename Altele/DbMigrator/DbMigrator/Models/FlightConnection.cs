using System;


namespace DbMigrator.Models
{
    public class FlightConnection
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid FlightId { get; set; }
    }
}
