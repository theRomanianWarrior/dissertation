using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation
{
    public class AllAttractionRecommendation
    {
        public AllAttractionRecommendation()
        {
            AttractionRecommendations = new HashSet<AttractionRecommendation>();
        }

        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }

        public virtual Agent.Agent SourceAgent { get; set; }
        public virtual ICollection<AttractionRecommendation> AttractionRecommendations { get; set; }
    }
}
