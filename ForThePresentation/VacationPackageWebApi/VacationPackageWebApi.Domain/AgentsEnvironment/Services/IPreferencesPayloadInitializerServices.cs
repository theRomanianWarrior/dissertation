using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IPreferencesPayloadInitializerServices
{
    public void FulfillCustomizedExpertAgentsRates(ref PreferencesRequest preferencesPayload);
}