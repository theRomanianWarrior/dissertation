namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightRecommendationResponse
{
    public FlightDirectionRecommendationBModel? FlightDirectionRecommendation { get; set; }
}