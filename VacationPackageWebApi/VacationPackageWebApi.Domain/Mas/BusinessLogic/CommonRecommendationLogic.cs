using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.Mas.BusinessLogic;

public static class CommonRecommendationLogic
{
    public const int Match = 1;
    public const int NoMatch = 0;
    
    public static TaskType AccessPreferencesAndChoseTask(object taskDistributionLock, TourismAgent agent, Dictionary<TaskType, double> customizedExpertAgentRates)
    {
        var taskTypeToWorkOn = TaskType.Default;

        lock (taskDistributionLock)
        {
            var availableTasks = GetListOfAvailableTasks();
            
            if (availableTasks == null) return TaskType.Default;
            if (!availableTasks.Any()) return TaskType.Default;

            foreach (var (key, _) in customizedExpertAgentRates.OrderByDescending(t => t.Value))
            {
                if (!availableTasks.Contains(key)) continue;
                taskTypeToWorkOn = key;
                availableTasks.Remove(key);
                RemoveAgentFromAvailableAgentsList(agent);
                agent.Status = false;
                break;
            }
        }

        return taskTypeToWorkOn;
    }

    public static Dictionary<TaskType, double> GetCurrentAgentCustomizedExpertRate(Guid currentAgentId, Dictionary<Guid, Dictionary<TaskType, double>> customizedExpertAgentRates)
    {
        return customizedExpertAgentRates.SingleOrDefault(r => r.Key == currentAgentId).Value;
    }

    private static List<TaskType>? GetListOfAvailableTasks()
    {
        return MasEnvironmentSingleton.Instance.Memory["AvailableTasks"];
    }
    
    public static Task SetPreferencesResponseStatusDone()
    {
        MasEnvironmentSingleton.Instance.Memory["PreferencesResponseStatus"] = "done";
        return Task.CompletedTask;
    }
    
    public static PreferencesRequest GetPreferencesPayload()
    {
        return MasEnvironmentSingleton.Instance.Memory["PreferencesPayload"];
    }
    
    public static Task SetPreferencesPayload(PreferencesRequest preferences)
    {
        MasEnvironmentSingleton.Instance.Memory["PreferencesPayload"] = preferences;
        return Task.CompletedTask;
    }
    
    private static void RemoveAgentFromAvailableAgentsList(TourismAgent agent)
    {
        (MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] as List<string>)!.Remove(agent.Name);
    }
    
    public static Task<List<string>> GetListOfAvailableAgentsAsync()
    {
        return Task.FromResult((MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] as List<string>)!);
    }
    
    public static Task<List<string>> GetListOfAllAgentsExceptCurrentAndCoordinator(TourismAgent tourismAgent)
    {
        var availableAgents = MasEnvironmentSingleton.Instance.AllAgents();
        if (availableAgents.Count > 2)
        {
            availableAgents.Remove(tourismAgent.Name);
            availableAgents.Remove("Coordinator");
        }

        return Task.FromResult(availableAgents.ToList());
    }
}