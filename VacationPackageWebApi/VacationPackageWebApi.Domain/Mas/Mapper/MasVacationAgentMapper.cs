using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.Mas.Mapper;

public static class MasVacationAgentMapper
{
    public static MasVacationAgent ToMasObject(this TourismAgent tourismAgent)
    {
        return new MasVacationAgent(new TourismAgent
        {
            Id = tourismAgent.Id,
            Name = tourismAgent.Name,
            Status = tourismAgent.Status,
            CurrentTask = tourismAgent.CurrentTask,
            ConfInd = tourismAgent.ConfInd
        });
    }
}