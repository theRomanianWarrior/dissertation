using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class ServiceEvaluation
    {
        public Guid Id { get; set; }
        public Guid FlightEvaluationId { get; set; }
        public Guid PropertyEvaluationId { get; set; }
        public Guid AttractionEvaluationId { get; set; }
    }
}
