using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.AgentsEnvironment.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Agent.Mapper;

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
}