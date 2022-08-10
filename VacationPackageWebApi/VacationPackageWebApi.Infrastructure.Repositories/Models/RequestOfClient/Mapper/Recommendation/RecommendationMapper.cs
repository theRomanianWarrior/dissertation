namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class RecommendationMapper
{
    public static MainResources.Recommendation.Recommendation ToEntity(Guid flightRecommendationId, Guid propertyRecommendationId, Guid attractionRecommendationId)
    {
        return new MainResources.Recommendation.Recommendation
        {
            Id = Guid.NewGuid(),
            FlightRecommendationId = flightRecommendationId,
            PropertyRecommendationId = propertyRecommendationId,
            AttractionRecommendationId = attractionRecommendationId
        };
    }
}