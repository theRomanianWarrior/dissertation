using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record AllAttractionEvaluationPoint
    {
        public Guid Id { get; set; }
        public short FinalPropertyEvaluation { get; set; }
    }
}
