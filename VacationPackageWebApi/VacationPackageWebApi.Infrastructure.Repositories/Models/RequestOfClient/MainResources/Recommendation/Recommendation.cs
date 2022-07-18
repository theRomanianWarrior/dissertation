using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation
{
    public class Recommendation
    {
        public Recommendation()
        {
            ClientRequests = new HashSet<ClientRequest>();
        }

        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid PropertyRecommendationId { get; set; }
        public Guid AttractionRecommendationId { get; set; }

        public virtual AttractionRecommendation AttractionRecommendation { get; set; }
        public virtual FlightRecommendation FlightRecommendation { get; set; }
        public virtual PropertyRecommendation PropertyRecommendation { get; set; }
        public virtual ICollection<ClientRequest> ClientRequests { get; set; }
    }
}
