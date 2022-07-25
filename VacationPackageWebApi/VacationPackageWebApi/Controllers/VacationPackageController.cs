using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VacationPackageWebApi.Application.Services;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.Property.Contracts;

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
            var recommendationPackage = await _preferencesPackageService.RequestVacationPackage(preferencesPayload);
            return Created(string.Empty,"Oke");
        }
    }
}
