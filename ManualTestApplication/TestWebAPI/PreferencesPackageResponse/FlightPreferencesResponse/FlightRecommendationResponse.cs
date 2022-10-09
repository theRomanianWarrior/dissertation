namespace TestWebAPI.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightRecommendationResponse
{
    public FlightDirectionRecommendationBModel? FlightDirectionRecommendation { get; set; }
}