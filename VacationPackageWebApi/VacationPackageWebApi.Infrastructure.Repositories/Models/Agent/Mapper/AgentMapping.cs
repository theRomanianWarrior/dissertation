using VacationPackageWebApi.Domain.AgentsEnvironment;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent.Mapper;

public static class AgentMapping
{
    public static TourismAgent ToBusinessModel(this Agent agent)
    {
        return new TourismAgent()
        {
            Id = agent.Id,
            Status = true,
            Name = agent.Name,
            CurrentTask = TaskType.Default,
            ConfInd = new Dictionary<TaskType, float>
            {
                {
                    TaskType.Flight,
                    agent.FlightSelfExpertRate
                },
                {
                    TaskType.Property,
                    agent.PropertySelfExpertRate
                },
                {
                    TaskType.Attractions,
                    agent.AttractionsSelfExpertRate
                }
            }
        };
    }
}