using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;

namespace VacationPackageWebApi.Application.Services
{
    public class PreferencesPackageService
    {
        private readonly IPreferencesPackageRequestRepository _preferencesPackageRepository;

        public PreferencesPackageService(IPreferencesPackageRequestRepository preferencesPackageRepository)
        {
            _preferencesPackageRepository = preferencesPackageRepository;
        }

        public async Task<PreferencesResponse> RequestVacationPackage(PreferencesRequest preferencesPayload)
        {
            await _preferencesPackageRepository.SavePreferences(preferencesPayload);
            return await _preferencesPackageRepository.GetFullPackageRecommendations(preferencesPayload);
        }
    }
}
