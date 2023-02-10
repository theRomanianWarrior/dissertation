using System;
using TestWebAPI.PreferencesPackageResponse.AttractionsPreferencesResponse;
using TestWebAPI.PreferencesPackageResponse.FlightPreferencesResponse;
using TestWebAPI.PreferencesPackageResponse.PropertyPreferencesResponse;

namespace TestWebAPI.PreferencesPackageResponse;

public record PreferencesResponse
{
    public Guid? ClientRequestId { get; set; }
    public FlightRecommendationResponse? FlightRecommendationResponse { get; set; }
    public PropertyRecommendationResponse? PropertyPreferencesResponse { get; set; }
    public AttractionsRecommendationResponse? AttractionsRecommendationResponse { get; set; }
}