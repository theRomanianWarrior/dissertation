namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation
{
    public class FlightConnection
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid FlightId { get; set; }

        public virtual Flight.Flight Flight { get; set; }
        public virtual FlightRecommendation FlightRecommendation { get; set; }
    }
}
