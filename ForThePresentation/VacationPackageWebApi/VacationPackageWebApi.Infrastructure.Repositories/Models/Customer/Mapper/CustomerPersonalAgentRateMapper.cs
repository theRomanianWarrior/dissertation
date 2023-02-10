using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Customer.Mapper;

public static class CustomerPersonalAgentRateMapper
{
    public static CustomerPersonalAgentRateBusinessModel ToBusinessModel(
        this CustomerPersonalAgentRate customerPersonalAgentRate)
    {
        return new CustomerPersonalAgentRateBusinessModel
        {
            AgentId = customerPersonalAgentRate.AgentId,
            ServiceExpertRate = new Dictionary<TaskType, float>
            {
                {
                    TaskType.Flight,
                    customerPersonalAgentRate.FlightExpertRate
                },
                {
                    TaskType.Property,
                    customerPersonalAgentRate.PropertyExpertRate
                },
                {
                    TaskType.Attractions,
                    customerPersonalAgentRate.AttractionsExpertRate
                }
            }
        };
    }
}