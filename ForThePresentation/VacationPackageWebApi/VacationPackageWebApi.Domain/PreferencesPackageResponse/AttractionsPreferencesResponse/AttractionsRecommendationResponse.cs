namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionsRecommendationResponse : BaseRecommendationBModel
{
    public List<AttractionRecommendationBModel> AttractionRecommendationList { get; set; }
}