using TestWebAPI.Property;

namespace TestWebAPI.PreferencesPackageResponse.PropertyPreferencesResponse;

public record PropertyRecommendationBModel : BaseRecommendationBModel
{
    public PropertyBusinessModel Property { get; set; }
}