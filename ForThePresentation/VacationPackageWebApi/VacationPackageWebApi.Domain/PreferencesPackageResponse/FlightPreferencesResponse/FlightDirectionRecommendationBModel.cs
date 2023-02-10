namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightDirectionRecommendationBModel
{
    public FlightRecommendationBModel? DepartureFlightRecommendation { get; set; }
    public FlightRecommendationBModel? ReturnFlightRecommendation { get; set; }
}