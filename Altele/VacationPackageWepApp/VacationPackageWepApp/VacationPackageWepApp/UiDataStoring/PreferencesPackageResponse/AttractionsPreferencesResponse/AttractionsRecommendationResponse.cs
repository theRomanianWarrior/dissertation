namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionsRecommendationResponse : BaseRecommendationBModel
{
    public List<AttractionRecommendationBModel> AttractionRecommendationList { get; set; }
}