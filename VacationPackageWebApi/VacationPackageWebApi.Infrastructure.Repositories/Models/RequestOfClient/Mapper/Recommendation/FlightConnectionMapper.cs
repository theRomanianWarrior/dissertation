using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class FlightConnectionMapper
{
    public static FlightConnection ToEntity(this FlightBusinessModel flight, Guid flightRecommendationId)
    {
        return new FlightConnection
        {
            Id = Guid.NewGuid(),
            FlightId = flight.Id,
            FlightRecommendationId = flightRecommendationId
        };
    }
}