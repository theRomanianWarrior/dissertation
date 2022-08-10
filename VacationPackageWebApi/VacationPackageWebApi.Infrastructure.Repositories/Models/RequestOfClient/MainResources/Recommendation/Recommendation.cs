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

        public virtual AllAttractionRecommendation AttractionRecommendation { get; set; }
        public virtual FlightDirectionRecommendation FlightRecommendation { get; set; }
        public virtual PropertyRecommendation PropertyRecommendation { get; set; }
        public virtual ICollection<ClientRequest> ClientRequests { get; set; }
    }
}
