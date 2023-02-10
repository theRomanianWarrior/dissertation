using VacationPackageWepApp.ServerModels.Attractions;

namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionRecommendationBModel
{
    public AttractionBusinessModel Attraction { get; set; }
}