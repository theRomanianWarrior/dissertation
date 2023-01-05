using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public record TourismAgent : AgentLocalDb
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; } // available
    public TaskType CurrentTask { get; set; } // the current chosen task
    public Dictionary<TaskType, float> ConfInd { get; set; } // confidence indexes of the agent to each type of task
    public List<TrustAgentRateBusinessModel>? TrustGradeInOtherAgent { get; set; }
}