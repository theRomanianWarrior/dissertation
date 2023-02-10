namespace VacationPackageWepApp.Models;

public class RecommendationUiModel
{
    public FlightUiModel FlightRecommendation { get; set; }
    public PropertyUiModel PropertyRecommendation { get; set; }
    public List<AttractionUiModel> AttractionsRecommendation { get; set; }
}