using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IPreferencesPackageService
{
    public Task<PreferencesResponse> RequestVacationPackage(PreferencesRequest preferencesPayload);
}