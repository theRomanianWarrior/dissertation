using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public class CustomerPersonalAgentRateBusinessModel
{
    public Guid AgentId { get; set; }
    public Dictionary<TaskType, float> ServiceExpertRate { get; set; }
}