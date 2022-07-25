using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.Services;

public class RecommendationService : IRecommendationService
{
    public Task<PreferencesResponse> GetFullRecommendationsPackage(PreferencesRequest preferencesPayload)
    {
        MasEnvironmentSingleton.Instance.Memory["preferencesPayload"] = preferencesPayload;
        MasCoordinatorSingleton.Instance.Broadcast("new_recommendation_request");
       // Wait the response from each agent of those 3 services ->
       // One flight agent, one attractions agent, one stay agent
    }

    private void Coordinator()
    {
        
    }
}