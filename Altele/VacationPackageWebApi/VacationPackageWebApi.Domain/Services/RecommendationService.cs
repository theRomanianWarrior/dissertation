﻿using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.Mas.Initializer;
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

        "____________________________________________________________".WriteDebug();
        MasCoordinatorSingleton.Instance.SendToMany(listOfAvailableAgents, "new_recommendation_request");

        cancellationTokenSource.CancelAfter(100000);

        await TimeoutFunctionHandler.CheckRecommendationReadyUntilSuccessOrTimeout(cancellationTokenSource.Token);

        if (cancellationTokenSource.IsCancellationRequested)
        {
            MasEnvVarsInitializer.ResetAll();
            cancellationTokenSource.TryReset();
            return null;
        }

        var readRecommendation = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;

        MasEnvVarsInitializer.ResetAll();

        return readRecommendation!;
    }
}