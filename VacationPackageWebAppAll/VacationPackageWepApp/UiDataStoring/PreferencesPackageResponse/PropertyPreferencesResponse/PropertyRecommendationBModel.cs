using VacationPackageWepApp.ServerModels.Property;

namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.PropertyPreferencesResponse;

public record PropertyRecommendationBModel : BaseRecommendationBModel
{
    public PropertyBusinessModel Property { get; set; }
}