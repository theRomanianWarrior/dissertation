using System;


namespace DbMigrator.Models
{
    public class TrustedAgentRate
    {
        public Guid Id { get; set; }
        public Guid FlightTrust { get; set; }
        public Guid PropertyTrust { get; set; }
        public Guid AttractionsTrust { get; set; }
    }
}
