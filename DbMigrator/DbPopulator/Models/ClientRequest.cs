using System;

namespace DbPopulator.Models
{
    public record ClientRequest
    {
        public Guid Id { get; set; }
        public Guid PreferencesPackageId { get; set; }
        public Guid RecommandationId { get; set; }
        public Guid Evaluation { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime RequestTimestamp { get; set; }
    }
}
