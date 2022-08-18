namespace VacationPackageWebApi.Domain.Mas.AgentsExpertBusinessModel;

public record AgentServiceRating
{
    public Guid AgentId { get; set; }
    public float ServiceRating { get; set; }
    public DateTime ServiceEvaluationDate { get; set; }
}