namespace DbPopulator.Models
{
    public record AttractionRecommendation
    {
        public Guid Id { get; set; }
        public string AttractionId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public Guid AllAttractionRecommendationId { get; set; }
    }
}
