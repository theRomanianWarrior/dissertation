using System.Collections.Generic;

namespace TestWebAPI.PreferencesPackageResponse.AttractionsPreferencesResponse;

public record AttractionsRecommendationResponse : BaseRecommendationBModel
{
    public List<AttractionRecommendationBModel> AttractionRecommendationList { get; set; }
}