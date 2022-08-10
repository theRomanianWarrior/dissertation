using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Mas;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.Mas.Initializer;
using VacationPackageWebApi.Domain.Mas.Mapper;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.Property;

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
            var masVacationAgents = (await _agentService.GetAllAgentsAsync()).Select(a => a.ToMasObject()).Take(3).ToList();

            var flights = (await _flightService.GetAllFlightsAsync()).ToHashSet();
            var properties = (await _propertyService.GetAllPropertiesAsync()).ToHashSet();
            var attractions = (await _attractionService.GetAllAttractionsAsync()).ToHashSet();

            InitializeAgentsLocalDatabase(masVacationAgents);

            StoreInAgentsLocalDbFlights(flights, masVacationAgents);
            StoreInAgentsLocalDbProperties(properties, masVacationAgents);
            StoreInAgentsLocalDbAttractions(attractions, masVacationAgents);

            AddAgentsToMasEnvironment(masVacationAgents);
        
            await MasEnvVarsInitializer.InitializeAll();
        
            MasEnvironmentSingleton.Instance.Add(MasCoordinatorSingleton.Instance, "Coordinator");
            
            foreach (var agent in masVacationAgents)
            {
                await CommonRecommendationLogic.InsertAgentNameToAvailableAgents(agent.Name);
            }
            
            MasEnvironmentSingleton.Instance.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void AddAgentsToMasEnvironment(List<MasVacationAgent> masVacationAgents)
    {
        foreach (var masAgent in masVacationAgents)
        {
            masAgent.Name = masAgent.TourismAgent.Name;
            MasEnvironmentSingleton.Instance.Add(masAgent);
        }    
    }
    
    private void InitializeAgentsLocalDatabase(List<MasVacationAgent> masVacationAgents)
    {
        foreach (var masAgent in masVacationAgents)
        {
            masAgent.TourismAgent.FlightsList = new HashSet<FlightBusinessModel>();
            masAgent.TourismAgent.AttractionsList = new HashSet<AttractionBusinessModel>();
            masAgent.TourismAgent.StaysList = new HashSet<PropertyBusinessModel>();
        }    
    }
    
    private void StoreInAgentsLocalDbFlights(HashSet<FlightBusinessModel> flights, List<MasVacationAgent> masVacationAgents)
    {
        foreach (var flight in flights)
        {
            foreach (var masAgent in masVacationAgents)
            {
                masAgent.TourismAgent.FlightsList.Add(flight);
            }
        }
    }
    
    private void StoreInAgentsLocalDbProperties(HashSet<PropertyBusinessModel> properties, List<MasVacationAgent> masVacationAgents)
    {
        foreach (var property in properties)
        {
            foreach (var masAgent in masVacationAgents)
            {
                masAgent.TourismAgent.StaysList.Add(property);
            }
        }
    }
    
    private void StoreInAgentsLocalDbAttractions(HashSet<AttractionBusinessModel> attractions, List<MasVacationAgent> masVacationAgents)
    {
        foreach (var attraction in attractions)
        {
            foreach (var masAgent in masVacationAgents)
            {
                masAgent.TourismAgent.AttractionsList.Add(attraction);
            }
        }
    }
    

}