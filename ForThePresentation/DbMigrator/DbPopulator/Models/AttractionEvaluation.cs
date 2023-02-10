namespace DbPopulator.Models
{
    public record AttractionEvaluation
    {
        public Guid Id { get; set; }
        public Guid AttractionPointId { get; set; }
        public string EvaluatedAttractionId { get; set; }
        public short Rate { get; set; }
    }
}
