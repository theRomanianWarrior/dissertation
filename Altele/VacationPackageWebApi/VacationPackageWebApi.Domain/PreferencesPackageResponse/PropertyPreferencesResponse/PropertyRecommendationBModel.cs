using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.PropertyPreferencesResponse;

public record PropertyRecommendationBModel : BaseRecommendationBModel
{
    public PropertyBusinessModel Property { get; set; }
}