using System;


namespace DbMigrator.Models
{
    public class TrustedAgent
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public Guid TrustedAgentId { get; set; }
        public Guid TrustedAgentRate { get; set; }
    }
}
