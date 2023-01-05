using System;


namespace DbMigrator.Models
{
    public class PropertyRecommendation
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid SourceAgentId { get; set; }
        public Guid InitialAssignedAgentId { get; set; }
    }
}
