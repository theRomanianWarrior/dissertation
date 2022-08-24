namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public class TrustServiceEvaluationBusinessModel
{
    public short PositiveEvaluation { get; set; }
    public short NegativeEvaluation { get; set; }
    public DateTime LastPositiveEvaluation { get; set; }
    public DateTime LastNegativeEvaluation { get; set; }
}