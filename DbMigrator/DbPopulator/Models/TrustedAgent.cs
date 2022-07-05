using System;

namespace DbPopulator.Models
{
    public record TrustedAgent
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public Guid TrustedAgentId { get; set; }
        public Guid TrustedAgentRate { get; set; }
    }
}
