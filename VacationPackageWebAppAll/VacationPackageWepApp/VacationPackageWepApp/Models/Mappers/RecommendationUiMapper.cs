using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;

namespace VacationPackageWepApp.Models.Mappers;

public static class RecommendationUiMapper
{
    public static RecommendationUiModel ToRecommendationUiModel(this PreferencesResponse preferencesResponse)
    {
        return new RecommendationUiModel
        {
            FlightRecommendation = preferencesResponse.ToFlightUiModel(),
            PropertyRecommendation = preferencesResponse.ToPropertyUiModel(),
            AttractionsRecommendation = preferencesResponse.ToAttractionsUiModel()
        };
    }
}