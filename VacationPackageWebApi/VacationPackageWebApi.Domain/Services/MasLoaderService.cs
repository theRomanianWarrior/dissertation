using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Mas.Initializer;
using VacationPackageWebApi.Domain.Mas.Mapper;
using VacationPackageWebApi.Domain.Mas.Singleton;

namespace VacationPackageWebApi.Domain.Services;

public class MasLoaderService : IMasLoaderService
{
    private readonly IAgentService _agentService;
    private readonly IFlightService _flightService;
    private readonly IPropertyService _propertyService;
    private readonly IAttractionService _attractionService;
    
    public MasLoaderService(IAgentService agentService, IFlightService flightService, IPropertyService propertyService, IAttractionService attractionService)
    {
        _agentService = agentService;
        _flightService = flightService;
        _propertyService = propertyService;
        _attractionService = attractionService;
    }
    
    public async Task LoadMasEnvironmentAsync()
    {
        try
        {
            var masVacationAgents = (await _agentService.GetAllAgentsAsync()).Select(a => a.ToMasObject()).ToList();

            var flights = (await _flightService.GetAllFlightsAsync()).ToHashSet();
            var properties = (await _propertyService.GetAllPropertiesAsync()).ToHashSet();
            var attractions = (await _attractionService.GetAllAttractionsAsync()).ToHashSet();
        
            foreach (var masAgent in masVacationAgents)
            {
                masAgent.TourismAgent.FlightsList = flights
                    .Where(f => f.StoredInLocalDbOfAgentWithId == masAgent.TourismAgent.Id).ToHashSet();
            
                masAgent.TourismAgent.StaysList = properties
                    .Where(p => p.StoredInLocalDbOfAgentWithId == masAgent.TourismAgent.Id).ToHashSet();
            
                masAgent.TourismAgent.AttractionsList = attractions
                    .Where(a => a.StoredInLocalDbOfAgentWithId == masAgent.TourismAgent.Id).ToHashSet();

                masAgent.Name = masAgent.TourismAgent.Name;
                MasEnvironmentSingleton.Instance.Add(masAgent);
            }
        
            await MasEnvVarsInitializer.InitializeAll();
        
            MasEnvironmentSingleton.Instance.Add(MasCoordinatorSingleton.Instance, "Coordinator");
            
            foreach (var agent in masVacationAgents)
            {
                await InsertAgentNameToAvailableAgents(agent.Name);
            }
            
            MasEnvironmentSingleton.Instance.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static Task InsertAgentNameToAvailableAgents(string agentName)
    {
        (MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] as List<string>)!.Add(agentName);
        return Task.CompletedTask;
    }
}