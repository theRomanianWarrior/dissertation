using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.Services;

public class PreferencesPayloadInitializerServices : IPreferencesPayloadInitializerServices
{
    private readonly IAgentService _agentService;

    public PreferencesPayloadInitializerServices(IAgentService agentService)
    {
        _agentService = agentService;
    }
    
    public void FulfillCustomizedExpertAgentsRates(ref PreferencesRequest preferencesPayload)
    {
        preferencesPayload.CustomizedExpertAgentRates ??= new Dictionary<Guid, Dictionary<TaskType, float>>();
                    
        var customerPersonalAgentsServicesRates = _agentService.GetCustomerPersonalAgentsServicesRates(preferencesPayload.CustomerId);
        foreach (var customerPersonalAgentServicesRates in customerPersonalAgentsServicesRates)
        {
            preferencesPayload.CustomizedExpertAgentRates.Add(customerPersonalAgentServicesRates.AgentId, customerPersonalAgentServicesRates.ServiceExpertRate);
        }     
    }
}