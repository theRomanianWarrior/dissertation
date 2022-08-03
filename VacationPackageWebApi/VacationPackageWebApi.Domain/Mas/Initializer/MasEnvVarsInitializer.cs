using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.Mas.Initializer;

public static class MasEnvVarsInitializer
{
    public static Task InitializeAll()
    {
        MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] = new List<string>();
        MasEnvironmentSingleton.Instance.Memory["AvailableTasks"] = new List<TaskType>
        {
            TaskType.Attractions,
            TaskType.Flight,
            TaskType.Property
        };

        MasEnvironmentSingleton.Instance.Memory["PreferencesPayload"] = new PreferencesRequest();
        MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] = new PreferencesResponse();
        MasEnvironmentSingleton.Instance.Memory["PreferencesResponseStatus"] = "undone";
        
        return Task.CompletedTask;
    }
}