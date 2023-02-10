namespace TestWebAPI.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightDirectionRecommendationBModel
{
    public FlightRecommendationBModel? DepartureFlightRecommendation { get; set; }
    public FlightRecommendationBModel? ReturnFlightRecommendation { get; set; }
}