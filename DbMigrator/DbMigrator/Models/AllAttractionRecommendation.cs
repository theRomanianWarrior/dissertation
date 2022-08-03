using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class AllAttractionRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
        public Guid InitialAssignedAgentId { get; set; }
    }
}
