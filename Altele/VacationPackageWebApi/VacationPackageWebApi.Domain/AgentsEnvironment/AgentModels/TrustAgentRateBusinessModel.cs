namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public class TrustAgentRateBusinessModel
{
    public string TrustedAgentName { get; set; }
    public TrustServiceEvaluationBusinessModel FlightTrust { get; set; }
    public TrustServiceEvaluationBusinessModel PropertyTrust { get; set; }
    public TrustServiceEvaluationBusinessModel AttractionsTrust { get; set; }
}