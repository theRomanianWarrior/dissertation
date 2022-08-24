using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Agent.Mapper;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories;

public class AgentRepository : IAgentRepository
{
    private readonly VacationPackageContext _context;

    public AgentRepository(VacationPackageContext context)
    {
        _context = context;
    }

    public async Task<List<TourismAgent>> GetAllAgentsAsync()
    {
        return await _context.Agents.Select(a => a.ToBusinessModel()).ToListAsync();
    }

    public List<CustomerPersonalAgentRateBusinessModel> GetCustomerPersonalAgentsServicesRates(Guid customerId)
    { 
        return _context.CustomerPersonalAgentRates.Where(r => r.CustomerId == customerId)
            .Select(rbm => rbm.ToBusinessModel()).ToList();
    }

    public Task InitializeCustomerPersonalAgentsRate(Guid customerId)
    {
        var agents = _context.Agents.ToList();
        foreach (var agent in agents)
        {
            var personalAgentRates = _context.CustomerPersonalAgentRates.Any(r => r.Agent.Id == agent.Id && r.CustomerId == customerId);
            if (!personalAgentRates)
            {
                var customerPersonalAgentRates = new CustomerPersonalAgentRate
                {
                    Id = Guid.NewGuid(),
                    AgentId = agent.Id,
                    CustomerId = customerId,
                    FlightExpertRate = 0.5f,
                    AttractionsExpertRate = 0.5f,
                    PropertyExpertRate = 0.5f
                };

                _context.CustomerPersonalAgentRates.Add(customerPersonalAgentRates);
            }
        }

        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Dictionary<Guid, List<TrustAgentRateBusinessModel>> GetAllAgentsTrustInOtherAgent()
    {
        var agents = _context.Agents.ToList();

        return agents.ToDictionary(agent => agent.Id, agent => GetTrustInOtherAgentsOfAgentWithId(agent.Id));
    }

    public string GetAgentNameById(Guid agentId)
    {
        return _context.Agents.Single(a => a.Id == agentId).Name;
    }
    
    public List<TrustAgentRateBusinessModel> GetTrustInOtherAgentsOfAgentWithId(Guid agentId)
    {
        var trustedAgentsRates = new List<TrustAgentRateBusinessModel>();
        var trustedAgents = _context.TrustedAgents.Where(a => a.AgentId == agentId)
            .Include(atf => atf.TrustedAgentRateNavigation).ThenInclude(tsf => tsf.FlightTrustNavigation)
            .Include(tsp => tsp.TrustedAgentRateNavigation).ThenInclude(sap => sap.PropertyTrustNavigation)
            .Include(tsa => tsa.TrustedAgentRateNavigation).ThenInclude(sac => sac.AttractionsTrustNavigation).ToList();

        foreach (var trustedAgentRate in trustedAgents)
        {
            trustedAgentsRates.Add(trustedAgentRate.ToBusinessModel(GetAgentNameById(trustedAgentRate.TrustedAgentId)));
        }

        return trustedAgentsRates;
    }
}