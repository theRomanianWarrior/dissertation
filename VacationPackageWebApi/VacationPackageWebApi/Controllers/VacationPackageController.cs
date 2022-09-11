using Microsoft.AspNetCore.Mvc;
using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;

namespace VacationPackageWebApi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VacationPackageController : Controller
    {
        private readonly IPreferencesPackageService _preferencesPackageService;
        private readonly IPreferencesPayloadInitializerServices _preferencesPayloadInitializerServices;
        
        public VacationPackageController(IPreferencesPackageService preferencesPackageService, IPreferencesPayloadInitializerServices preferencesPayloadInitializerServices)
        {
            _preferencesPackageService = preferencesPackageService;
            _preferencesPayloadInitializerServices = preferencesPayloadInitializerServices;
        }

        [HttpPost]
        public async Task<IActionResult> RequestVacationRecommendation([FromBody] PreferencesRequest preferencesPayload)
        {
            var clientRequest = new ClientRequest
            {
                Id = Guid.NewGuid(),
                RequestTimestamp = DateTime.Now
            };

            _preferencesPayloadInitializerServices.FulfillCustomizedExpertAgentsRates(ref preferencesPayload);
            
            var recommendationPackage = await _preferencesPackageService.RequestVacationPackage(preferencesPayload, clientRequest.Id, clientRequest.RequestTimestamp);
            UserReportHelper.WriteUserPreferencesRequest(preferencesPayload, clientRequest.RequestTimestamp);
            
            if (recommendationPackage is {AttractionsRecommendationResponse: { }, FlightRecommendationResponse: { }, PropertyPreferencesResponse: { }})
                Task.Factory.StartNew(_preferencesPackageService.SaveRecommendationResponse(recommendationPackage, clientRequest.Id));

            recommendationPackage!.ClientRequestId = clientRequest.Id;

            return new JsonResult(recommendationPackage);
            //var returnedRecommendationPackage = JsonConvert.SerializeObject(recommendationPackage);
            //return returnedRecommendationPackage;
        }
        
        [HttpPost]
        public async Task<ActionResult> SaveEvaluations([FromBody] ServiceEvaluationDto serviceEvaluation)
        {
            await _preferencesPackageService.SaveEvaluation(serviceEvaluation);
            UserReportHelper.ClearCurrentProcessingAgentSelfExpertLogData();
            return new JsonResult("Success");
        }
    }
}
