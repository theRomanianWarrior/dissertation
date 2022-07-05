using System;

namespace DbPopulator.Models
{
    public record PropertyRecommandation
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
    }
}
