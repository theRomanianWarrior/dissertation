using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public record TrustServiceEvaluation
    {
        public Guid Id { get; set; }
        public short PositiveEvaluation { get; set; }
        public short NegativeEvaluation { get; set; }
        public short NeutralEvaluation { get; set; }
        public DateTime LastPositiveEvaluation { get; set; }
        public DateTime LastNegativeEvaluation { get; set; }
    }
}
