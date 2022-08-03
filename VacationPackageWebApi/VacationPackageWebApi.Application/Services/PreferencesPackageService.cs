using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Application.Services
{
    public class PreferencesPackageService : IPreferencesPackageService
    {
        private readonly IPreferencesPackageRequestRepository _preferencesPackageRepository;
        private readonly IRecommendationService _recommendationService;
        
        public PreferencesPackageService(IPreferencesPackageRequestRepository preferencesPackageRepository, IRecommendationService recommendationService)
        {
            _preferencesPackageRepository = preferencesPackageRepository;
            _recommendationService = recommendationService;
        }
        
        public async Task<PreferencesResponse?> RequestVacationPackage(PreferencesRequest preferencesPayload)
        {
            await _preferencesPackageRepository.SavePreferences(preferencesPayload);
            return await _recommendationService.GetFullRecommendationsPackage(preferencesPayload);
        }
    }
}
