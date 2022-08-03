namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionsRecommendationResponse
{
    public List<AttractionRecommendationBModel> AttractionRecommendationList { get; set; }
}