using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;

public interface IAgentRepository
{
    public Task<List<TourismAgent>> GetAllAgentsAsync();
}