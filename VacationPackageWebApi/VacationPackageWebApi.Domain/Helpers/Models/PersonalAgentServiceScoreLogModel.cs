namespace VacationPackageWebApi.Domain.Helpers.Models;

public class PersonalAgentServiceScoreLogModel
{
    public string AgentName { get; set; }
    public int FlightRecommendationsDoneForCurrentUser { get; set; }
    public int PropertyRecommendationsDoneForCurrentUser { get; set; }
    public int AttractionsRecommendationsDoneForCurrentUser { get; set; }

}