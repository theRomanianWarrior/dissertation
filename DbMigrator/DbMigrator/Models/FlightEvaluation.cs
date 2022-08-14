using System;


namespace DbMigrator.Models
{
    public class FlightEvaluation
    {
        public Guid Id { get; set; }
        public bool Class { get; set; }
        public bool Price { get; set; }
        public bool Company { get; set; }
        public bool FlightDate { get; set; }
        public bool FlightTime { get; set; }
        public float FlightRating { get; set; }
    }
}
