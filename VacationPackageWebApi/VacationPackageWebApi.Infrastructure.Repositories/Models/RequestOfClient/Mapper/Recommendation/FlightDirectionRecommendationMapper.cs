using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class FlightDirectionRecommendationMapper
{
    public static FlightDirectionRecommendation ToEntity(this FlightDirectionRecommendationBModel flightDirectionRecommendation, Guid departureFlightRecommendationId, Guid returnFlightRecommendationId)
    {
        return new FlightDirectionRecommendation
        {
            Id = Guid.NewGuid(),
            Departure = departureFlightRecommendationId,
            Return = returnFlightRecommendationId
        };
    }
}