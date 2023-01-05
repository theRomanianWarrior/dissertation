using System;

namespace DbMigrator.Models
{
    public class AttractionEvaluation
    {
        public Guid Id { get; set; }
        public Guid AttractionPointId { get; set; }
        public string EvaluatedAttractionId { get; set; }
        public bool Rate { get; set; }
    }
}
