using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.AttractionsPreferencesResponse;
using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.FlightPreferencesResponse;
using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.PropertyPreferencesResponse;

namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;

public record PreferencesResponse
{
    public Guid? ClientRequestId { get; set; }
    public FlightRecommendationResponse? FlightRecommendationResponse { get; set; }
    public PropertyRecommendationResponse? PropertyPreferencesResponse { get; set; }
    public AttractionsRecommendationResponse? AttractionsRecommendationResponse { get; set; }
}