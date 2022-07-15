using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record PropertyRecommendation
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }

        public virtual Property Property { get; set; }
        public virtual Agent.Agent SourceAgent { get; set; }
    }
}
