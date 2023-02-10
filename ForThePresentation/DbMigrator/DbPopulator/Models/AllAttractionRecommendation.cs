namespace DbPopulator.Models
{
    public record AllAttractionRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
    }
}
