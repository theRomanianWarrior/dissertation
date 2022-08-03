using VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.PropertyPreferencesResponse;

namespace VacationPackageWebApi.Domain.PreferencesPackageResponse;

public record PreferencesResponse
{
    public FlightRecommendationResponse? FlightRecommendationResponse { get; set; }
    public PropertyRecommendationResponse? PropertyPreferencesResponse { get; set; }
    public AttractionsRecommendationResponse? AttractionsRecommendationResponse { get; set; }
}