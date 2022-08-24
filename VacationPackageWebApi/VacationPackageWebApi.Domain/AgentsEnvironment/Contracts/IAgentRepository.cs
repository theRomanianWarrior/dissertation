using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;

public interface IAgentRepository
{
    public Task<List<TourismAgent>> GetAllAgentsAsync();
    public List<CustomerPersonalAgentRateBusinessModel> GetCustomerPersonalAgentsServicesRates(Guid customerId);
    public Task InitializeCustomerPersonalAgentsRate(Guid customerId);
    public List<TrustAgentRateBusinessModel> GetTrustInOtherAgentsOfAgentWithId(Guid agentId);
    public Dictionary<Guid, List<TrustAgentRateBusinessModel>> GetAllAgentsTrustInOtherAgent();
}