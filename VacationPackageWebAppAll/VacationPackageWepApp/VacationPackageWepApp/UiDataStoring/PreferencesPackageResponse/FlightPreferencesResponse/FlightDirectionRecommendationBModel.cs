using Newtonsoft.Json;

namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightDirectionRecommendationBModel
{
    public FlightRecommendationBModel? DepartureFlightRecommendation { get; set; }
    public FlightRecommendationBModel? ReturnFlightRecommendation { get; set; }
}