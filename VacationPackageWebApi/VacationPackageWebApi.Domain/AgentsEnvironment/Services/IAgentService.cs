using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IAgentService
{
    public Task<List<TourismAgent>> GetAllAgentsAsync();
}