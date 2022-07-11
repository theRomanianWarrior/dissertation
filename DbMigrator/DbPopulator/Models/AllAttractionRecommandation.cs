using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record AllAttractionRecommandation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
    }
}
