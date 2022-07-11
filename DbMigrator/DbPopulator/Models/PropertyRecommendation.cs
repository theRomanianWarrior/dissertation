using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record PropertyRecommendation
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
    }
}
