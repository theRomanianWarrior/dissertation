using VacationPackageWebApi.Domain.Attractions;

namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionRecommendationBModel : BaseRecommendationBModel
{
    public AttractionBusinessModel Attraction { get; set; }
}