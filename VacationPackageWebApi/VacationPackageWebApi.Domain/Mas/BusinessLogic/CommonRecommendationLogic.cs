﻿using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.Mas.BusinessLogic;

public static class CommonRecommendationLogic
{
    public const int Match = 1;
    public const int NoMatch = 0;
    private static readonly Random Random = new();
    
    public static TaskType AccessPreferencesAndChoseTask(object taskDistributionLock, TourismAgent agent)
    {
        lock (taskDistributionLock)
        {
            var availableTasks = GetListOfAvailableTasks()?.ToList();
            
            if (availableTasks == null) return TaskType.Default;
            if (!availableTasks.Any()) return TaskType.Default;

            if (agent.ConfInd[TaskType.Flight] == 0 && agent.ConfInd[TaskType.Property] == 0 &&
                agent.ConfInd[TaskType.Attractions] == 0)
            {
                var randomIndexTaskType = Random.Next(availableTasks.Count);
                RemoveTaskFromAvailableTasks(availableTasks[randomIndexTaskType]);
                RemoveAgentFromAvailableAgentsList(agent.Name);
                return availableTasks[randomIndexTaskType];
                
            }
            
            foreach (var (key, _) in agent.ConfInd.OrderByDescending(t => t.Value))
            {
                if (!availableTasks.Contains(key)) continue;
                RemoveTaskFromAvailableTasks(key);
                RemoveAgentFromAvailableAgentsList(agent.Name);
                agent.Status = false;
                return key;
            }
        }

        return TaskType.Default;
    }

    public static void StoreAgentsTrustRate(Dictionary<Guid, List<TrustAgentRateBusinessModel>> agentsTrustRates)
    {
        MasEnvironmentSingleton.Instance.Memory["AgentsTrustRates"] = agentsTrustRates;
    }

    public static List<TrustAgentRateBusinessModel>? GetAgentTrustRateOfAgentWithId(Guid id)
    {
        return (MasEnvironmentSingleton.Instance.Memory["AgentsTrustRates"] as
            Dictionary<Guid, List<TrustAgentRateBusinessModel>?>)?[id];
    }
    
    public static Dictionary<TaskType, float> GetCurrentAgentCustomizedExpertRate(Guid currentAgentId, Dictionary<Guid, Dictionary<TaskType, float>> customizedExpertAgentRates)
    {
         if(customizedExpertAgentRates.Count != 0)
            return customizedExpertAgentRates.SingleOrDefault(r => r.Key == currentAgentId).Value;

         var defaultCustomizedExpertRate = new Dictionary<TaskType, float>();
         for (var taskType = TaskType.Flight; taskType < TaskType.Default; taskType++)
         {
             defaultCustomizedExpertRate.Add(taskType, 0.0f);
         }

         return defaultCustomizedExpertRate;
    }

    private static List<TaskType>? GetListOfAvailableTasks()
    {
        return MasEnvironmentSingleton.Instance.Memory["AvailableTasks"];
    }
    
    private static void RemoveTaskFromAvailableTasks(TaskType task)
    {
        (MasEnvironmentSingleton.Instance.Memory["AvailableTasks"] as List<TaskType>)!.Remove(task);
    }
    
    public static Task InsertAgentNameToAvailableAgents(string agentName)
    {
        (MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] as List<string>)!.Add(agentName);
        return Task.CompletedTask;
    }
    
    public static void SetPreferencesResponseStatusDone()
    {
        MasEnvironmentSingleton.Instance.Memory["PreferencesResponseStatus"] = "done";
    }

    public static bool IsDepartureFlightRecommendationDone()
    {
        var preferencesResponse = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;
        return preferencesResponse?.FlightRecommendationResponse?.FlightDirectionRecommendation?.DepartureFlightRecommendation != null;
    }

    public static bool IsReturnFlightRecommendationDone()
    {
        var preferencesResponse = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;
        return preferencesResponse?.FlightRecommendationResponse?.FlightDirectionRecommendation?.ReturnFlightRecommendation != null;
    }
    
    public static bool IsPropertyRecommendationDone()
    {
        var preferencesResponse = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;
        return preferencesResponse?.PropertyPreferencesResponse?.PropertyRecommendationBModel != null;
    }
    
    public static bool IsAttractionsRecommendationDone()
    {
        var preferencesResponse = MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse;
        return preferencesResponse?.AttractionsRecommendationResponse != null;
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
    
    public static void RemoveAgentFromAvailableAgentsList(string name)
    {
        (MasEnvironmentSingleton.Instance.Memory["AvailableAgents"] as List<string>)!.Remove(name);
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