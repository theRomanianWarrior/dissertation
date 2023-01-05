using VacationPackageWebApi.Domain.Attractions;

namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionRecommendationBModel
{
    public AttractionBusinessModel Attraction { get; set; }
}