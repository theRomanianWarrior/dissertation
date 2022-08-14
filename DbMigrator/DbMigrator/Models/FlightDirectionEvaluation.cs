using System;


namespace DbMigrator.Models
{
    public class FlightDirectionEvaluation
    {
        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }
        public float TotalFlightRating { get; set; }
    }
}
