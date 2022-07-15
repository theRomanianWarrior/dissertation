using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record AttractionRecommendation
    {
        public Guid Id { get; set; }
        public string AttractionId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public Guid AllAttractionRecommendationId { get; set; }

        public virtual AllAttractionRecommendation AllAttractionRecommendation { get; set; }
        public virtual OpenTripMapAttraction Attraction { get; set; }
        public virtual Agent.Agent SourceAgent { get; set; }
    }
}
