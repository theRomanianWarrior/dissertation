using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record FlightRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public DateOnly FlightDate { get; set; }
        public short Stops { get; set; }

        public virtual Agent.Agent SourceAgent { get; set; }
    }
}
