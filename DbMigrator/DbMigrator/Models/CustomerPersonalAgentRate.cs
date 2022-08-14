using System;


namespace DbMigrator.Models
{
    public class CustomerPersonalAgentRate
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AgentId { get; set; }
    }
}
