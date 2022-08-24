namespace VacationPackageWebApi.Domain.Helpers.Models;

public record PersonalAgentRateLogModel
{
    public string AgentName { get; set; }
    public float FlightExpertRate { get; set; }
    public float PropertyExpertRate { get; set; }
    public float AttractionsExpertRate { get; set; }
}