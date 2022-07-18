using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation
{
    public class AttractionRecommendation
    {
        public AttractionRecommendation()
        {
            Recommendations = new HashSet<Recommendation>();
        }

        public Guid Id { get; set; }
        public string AttractionId { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public Guid AllAttractionRecommendationId { get; set; }

        public virtual AllAttractionRecommendation AllAttractionRecommendation { get; set; }
        public virtual OpenTripMapAttraction Attraction { get; set; }
        public virtual Agent.Agent SourceAgent { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
    }
}
