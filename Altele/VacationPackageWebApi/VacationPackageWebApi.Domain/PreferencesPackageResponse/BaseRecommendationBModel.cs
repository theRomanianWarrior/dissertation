namespace VacationPackageWebApi.Domain.PreferencesPackageResponse;

public record BaseRecommendationBModel
{
    public Guid SourceAgentId { get; set; }
    public string InitialAssignedAgentName { get; set; }
    public string Status { get; set; }
}