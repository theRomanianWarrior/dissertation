using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IRecommendationService
{
    public Task<PreferencesResponse> GetFullRecommendationsPackage(PreferencesRequest preferencesPayload);
}