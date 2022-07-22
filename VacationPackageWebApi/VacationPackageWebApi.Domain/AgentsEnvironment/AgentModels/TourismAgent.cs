using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public record TourismAgent : AgentLocalDb
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; } // available
    public Guid? CurrentTask { get; set; } // the current chosen task
    public Dictionary<TaskType, double> ConfInd { get; set; } // confidence indexes of the agent to each type of task
}