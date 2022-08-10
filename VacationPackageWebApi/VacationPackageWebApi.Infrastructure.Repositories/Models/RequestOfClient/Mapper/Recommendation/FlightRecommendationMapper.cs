using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class FlightRecommendationMapper
{
    public static FlightRecommendation ToEntity(this FlightRecommendationBModel flightRecommendation, Guid initialAssignedAgentId)
    {
        return new FlightRecommendation
        {
            Id = Guid.NewGuid(),
            SourceAgentId = flightRecommendation.SourceAgentId, 
            InitialAssignedAgentId = initialAssignedAgentId,
            FlightDate = DateOnly.FromDateTime(flightRecommendation.FlightDate), 
            Stops = flightRecommendation.Stops, 
            Time = TimeOnly.FromDateTime(flightRecommendation.DepartureTime)
        };
    }
}