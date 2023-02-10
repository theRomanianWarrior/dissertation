using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
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
        MasEnvironmentSingleton.Instance.Memory["AgentsTrustRates"] =
            new Dictionary<Guid, List<TrustAgentRateBusinessModel>>();
        return Task.CompletedTask;
    }

    public static void ResetAll()
    {
        var allAgents = MasEnvironmentSingleton.Instance.AllAgents();
        if (allAgents.Contains("Coordinator"))
            allAgents.Remove("Coordinator");
        MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] = allAgents;
        MasEnvironmentSingleton.Instance.Memory["AvailableTasks"] = new List<TaskType>
        {
            TaskType.Attractions,
            TaskType.Flight,
            TaskType.Property
        };

        MasEnvironmentSingleton.Instance.Memory["PreferencesPayload"] = new PreferencesRequest();
        MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] = new PreferencesResponse();
        MasEnvironmentSingleton.Instance.Memory["AgentsTrustRates"] =
            new Dictionary<Guid, List<TrustAgentRateBusinessModel>>();
    }

    public static void ResetTourismAgent(ref TourismAgent tourismAgent)
    {
        tourismAgent.Status = true;
        tourismAgent.CurrentTask = TaskType.Default;
    }
}