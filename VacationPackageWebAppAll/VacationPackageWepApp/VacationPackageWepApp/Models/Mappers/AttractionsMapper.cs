using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;

namespace VacationPackageWepApp.Models.Mappers;

public static class AttractionsMapper
{
    public static List<AttractionUiModel> ToAttractionsUiModel(this PreferencesResponse preferencesResponse)
    {
        var uiAttraction = new List<AttractionUiModel>();
        foreach (var attraction in preferencesResponse.AttractionsRecommendationResponse.AttractionRecommendationList)
        {
            var kinds = attraction.Attraction.Kinds.Split(',');
            var refactoredKinds = "";
            foreach (var kind in kinds)
            {
                refactoredKinds += "#" + kind + " ";
            }

            uiAttraction.Add(
                new AttractionUiModel()
                {
                    Name = attraction.Attraction.Name, 
                    Address = attraction.Attraction.Pedestrian + ", " + attraction.Attraction.Postcode + ", " + attraction.Attraction.Town + ", " + attraction.Attraction.Country, 
                    Rate = attraction.Attraction.Rate, 
                    KindsTags = refactoredKinds
                });
        }

        return uiAttraction;
    }
}