namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightRecommendationBModel : BaseRecommendationBModel
{
    public DateTime FlightDate { get; set; }
    public short Stops { get; set; }
    public DateTime DepartureTime { get; set; }
    public short FlightClass { get; set; }
    public List<FlightConnectionBModel> FlightConnection { get; set; }
    
}