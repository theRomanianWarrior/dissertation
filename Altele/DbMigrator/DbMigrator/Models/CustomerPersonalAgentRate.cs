using System;


namespace DbMigrator.Models
{
    public class CustomerPersonalAgentRate
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AgentId { get; set; }
        public float FlightExpertRate { get; set; }
        public float PropertyExpertRate { get; set; }
        public float AttractionsExpertRate { get; set; }
    }
}
