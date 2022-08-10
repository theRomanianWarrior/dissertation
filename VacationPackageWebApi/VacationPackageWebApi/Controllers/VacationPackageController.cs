using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;

namespace VacationPackageWebApi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VacationPackageController : Controller
    {
        private readonly IPreferencesPackageService _preferencesPackageService;
        private readonly IAgentService _agentService;
        private readonly IFlightService _flightService;
        private readonly IPropertyService _propertyService;
        private readonly IAttractionService _attractionService;
        private readonly IMasLoaderService _masLoaderService;

        public VacationPackageController(IPreferencesPackageService preferencesPackageService, IAgentService agentService, IFlightService flightService, IPropertyService propertyService, IAttractionService attractionService, IMasLoaderService masLoaderService)
        {
            _preferencesPackageService = preferencesPackageService;
            _agentService = agentService;
            _flightService = flightService;
            _propertyService = propertyService;
            _attractionService = attractionService;
            _masLoaderService = masLoaderService;
        }

        [HttpPost]
        public async Task<ActionResult> RequestVacationRecommendation([FromBody] PreferencesRequest preferencesPayload)
        {
            var clientRequest = new ClientRequest()
            {
                Id = Guid.NewGuid(),
                RequestTimestamp = DateTime.Now
            };
            
            var recommendationPackage = await _preferencesPackageService.RequestVacationPackage(preferencesPayload, clientRequest.Id, clientRequest.RequestTimestamp);
            if (recommendationPackage != null)
                Task.Factory.StartNew(_preferencesPackageService.SaveRecommendationResponse(recommendationPackage, clientRequest.Id));
            
            Console.WriteLine(JsonConvert.SerializeObject(recommendationPackage, Formatting.Indented));

            return Created(string.Empty, recommendationPackage);
        }
    }
}
