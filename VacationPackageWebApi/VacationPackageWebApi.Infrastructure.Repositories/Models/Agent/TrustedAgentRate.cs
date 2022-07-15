using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public record TrustedAgentRate
    {
        public Guid Id { get; set; }
        public Guid FlightTrust { get; set; }
        public Guid PropertyTrust { get; set; }
        public Guid AttractionsTrust { get; set; }

        public virtual TrustServiceEvaluation AttractionsTrustNavigation { get; set; }
        public virtual TrustServiceEvaluation FlightTrustNavigation { get; set; }
        public virtual TrustServiceEvaluation PropertyTrustNavigation { get; set; }
    }
}
