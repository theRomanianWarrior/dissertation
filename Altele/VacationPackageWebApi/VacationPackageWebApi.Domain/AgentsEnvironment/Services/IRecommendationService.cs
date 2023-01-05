using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IRecommendationService
{
    public Task<PreferencesResponse?> GetFullRecommendationsPackage(PreferencesRequest preferencesPayload);
}