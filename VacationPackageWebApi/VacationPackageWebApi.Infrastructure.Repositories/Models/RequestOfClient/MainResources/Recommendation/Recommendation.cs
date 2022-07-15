namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record Recommendation
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid PropertyRecommendationId { get; set; }
        public Guid AttractionRecommendationId { get; set; }

        public virtual AttractionRecommendation AttractionRecommendation { get; set; }
        public virtual FlightRecommendation FlightRecommendation { get; set; }
        public virtual PropertyRecommendation PropertyRecommendation { get; set; }
    }
}
