using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IAgentService
{
    public Task<List<TourismAgent>> GetAllAgentsAsync();

    public Task InitializeCustomerPersonalAgentsRate(Guid customerId);

    public List<CustomerPersonalAgentRateBusinessModel> GetCustomerPersonalAgentsServicesRates(
        Guid customerId);

    public List<TrustAgentRateBusinessModel> GetTrustInOtherAgentsOfAgentWithId(Guid agentId);
    public Dictionary<Guid, List<TrustAgentRateBusinessModel>> GetAllAgentsTrustInOtherAgent();
}