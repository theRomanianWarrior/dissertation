using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.Services;

public class RecommendationService : IRecommendationService
{
    public async Task<PreferencesResponse?> GetFullRecommendationsPackage(PreferencesRequest preferencesPayload)
    {
        var listOfAvailableAgents = await CommonRecommendationLogic.GetListOfAvailableAgentsAsync();
        var cancellationTokenSource = new CancellationTokenSource();

        await CommonRecommendationLogic.SetPreferencesPayload(preferencesPayload);
        
        MasCoordinatorSingleton.Instance.SendToMany(listOfAvailableAgents, "new_recommendation_request");

        cancellationTokenSource.CancelAfter(30000*100);

        await TimeoutFunctionHandler.CheckRecommendationReadyUntilSuccessOrTimeout(cancellationTokenSource.Token);

        if (MasEnvironmentSingleton.Instance.Memory["PreferencesResponseStatus"] != "done") return null!; // was canceled
        var readRecommendation = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;
        return readRecommendation!;
    }
}