using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VacationPackageWebApi.Application.Services;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VacationPackageController : Controller
    {
        private readonly PreferencesPackageService _preferencesPackageService;

        public VacationPackageController(PreferencesPackageService preferencesPackageService)
        {
            _preferencesPackageService = preferencesPackageService;
        }

        [HttpPost]
        public async Task<ActionResult> RequestVacationRecommendation([FromBody] PreferencesRequest preferencesPayload)
        {
            var recommandationPackage = await _preferencesPackageService.RequestVacationPackage(preferencesPayload);
            return Created(string.Empty,"Oke");
        }
    }
}
