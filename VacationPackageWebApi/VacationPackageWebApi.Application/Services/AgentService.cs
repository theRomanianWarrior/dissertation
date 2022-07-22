using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;

namespace VacationPackageWebApi.Application.Services;

public class AgentService : IAgentService
{
    private readonly IAgentRepository _agentRepository;
    
    public AgentService(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<List<TourismAgent>> GetAllAgentsAsync()
    {
        return await _agentRepository.GetAllAgentsAsync();
    }
}