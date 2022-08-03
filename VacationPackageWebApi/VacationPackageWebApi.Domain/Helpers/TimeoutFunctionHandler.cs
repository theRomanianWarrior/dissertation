using VacationPackageWebApi.Domain.Mas.Singleton;

namespace VacationPackageWebApi.Domain.Helpers;

public static class TimeoutFunctionHandler
{

    public static async Task CheckRecommendationReadyUntilSuccessOrTimeout(CancellationToken cancellationToken)
    {
        try
        {

            await Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(50, cancellationToken);
                    if (MasEnvironmentSingleton.Instance.Memory["PreferencesResponseStatus"] == "done")
                    {
                        return;
                    }
                }
            }, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            if(!cancellationToken.IsCancellationRequested) throw;
        }
    }
}