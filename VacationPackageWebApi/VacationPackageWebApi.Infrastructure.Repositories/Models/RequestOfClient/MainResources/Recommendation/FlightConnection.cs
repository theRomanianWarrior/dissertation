namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record FlightConnection
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid FlightId { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual FlightRecommendation FlightRecommendation { get; set; }
    }
}
