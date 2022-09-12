namespace VacationPackageWebApi.Domain.Helpers.Models;

public class PersonalAgentSelfExpertRateLogModel
{
    public string AgentName { get; set; }
    public string DateOfRequest { get; set; }
    public int DaysDifferenceFromToday { get; set; }
    public string ServiceType { get; set; }
    public SelfExpertRatePropertiesLogModel ExpertServiceRatings { get; set; }
}