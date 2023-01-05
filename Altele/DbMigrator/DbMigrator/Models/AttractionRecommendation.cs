using System;


namespace DbMigrator.Models
{
    public class AttractionRecommendation
    {
        public Guid Id { get; set; }
        public string AttractionId { get; set; }
        public Guid AllAttractionRecommendationId { get; set; }
    }
}
