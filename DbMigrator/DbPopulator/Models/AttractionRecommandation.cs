using System;

namespace DbPopulator.Models
{
    public record AttractionRecommandation
    {
        public Guid Id { get; set; }
        public Guid AttractionId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public Guid AllAttractionRecommandationId { get; set; }
    }
}
