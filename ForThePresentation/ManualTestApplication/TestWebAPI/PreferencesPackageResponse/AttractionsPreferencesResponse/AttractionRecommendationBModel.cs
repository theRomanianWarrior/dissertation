using TestWebAPI.Attractions;

namespace TestWebAPI.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionRecommendationBModel
{
    public AttractionBusinessModel Attraction { get; set; }
}