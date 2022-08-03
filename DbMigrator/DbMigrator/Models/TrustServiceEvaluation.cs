using System;
using System.Collections.Generic;

namespace DbMigrator.Models
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
