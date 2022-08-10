using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IPreferencesPackageService
{
    public Task<PreferencesResponse?> RequestVacationPackage(PreferencesRequest preferencesPayload, Guid clientRequestId, DateTime requestTimestamp);
    public Action SaveRecommendationResponse(PreferencesResponse preferencesResponse, Guid clientRequestId);
}