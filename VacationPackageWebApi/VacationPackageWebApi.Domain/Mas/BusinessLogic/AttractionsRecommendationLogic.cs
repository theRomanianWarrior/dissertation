using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;

namespace VacationPackageWebApi.Domain.Mas.BusinessLogic;

public static class AttractionsRecommendationLogic
{
    private static readonly Random Random = new();

    public static bool FindOptimalAttractionAndStoreInMemory(Guid sourceAgentId,
        string initialAssignedAgentName, object recommendationPopulationLock, TourismAgent tourismAgent,
        PreferencesRequest preferencesRequest)
    {
        var cityAttractions = FindAttractionsByCity(tourismAgent, preferencesRequest.DestinationCityNavigation.Name);

        if (cityAttractions.Any())
        {
            var topAttractions =
                FindOptimalTopAttractionsBasedOnUserPreferences(preferencesRequest, cityAttractions);

            var attractionsRecommendationList = ConvertToAttractionRecommendationBModelList(topAttractions);

            StoreInMemoryAttractionsRecommendations(recommendationPopulationLock, sourceAgentId,
                initialAssignedAgentName, attractionsRecommendationList);
            return true;
        }

        return false;
    }

    private static void StoreInMemoryAttractionsRecommendations(object recommendationPopulationLock, Guid sourceAgentId,
        string initialAssignedAgentName, List<AttractionRecommendationBModel> attractionsRecommendationList)
    {
        var attractionsRecommendationResponse =
            PopulateAttractionsRecommendationResponse(attractionsRecommendationList);
        attractionsRecommendationResponse.SourceAgentId = sourceAgentId;
        attractionsRecommendationResponse.InitialAssignedAgentName = initialAssignedAgentName;

        lock (recommendationPopulationLock)
        {
            (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!
                .AttractionsRecommendationResponse ??= attractionsRecommendationResponse;
        }
    }

    private static AttractionsRecommendationResponse PopulateAttractionsRecommendationResponse(
        List<AttractionRecommendationBModel> attractionsRecommendationList)
    {
        return new AttractionsRecommendationResponse
        {
            AttractionRecommendationList = attractionsRecommendationList
        };
    }

    private static List<AttractionRecommendationBModel> ConvertToAttractionRecommendationBModelList(
        List<AttractionBusinessModel> topAttractions)
    {
        return topAttractions.Select(attraction => new AttractionRecommendationBModel {Attraction = attraction})
            .ToList();
    }

    private static List<AttractionBusinessModel> FindOptimalTopAttractionsBasedOnUserPreferences(
        PreferencesRequest preferencesRequest, HashSet<AttractionBusinessModel> cityAttractions)
    {
        var topAttractions = new List<AttractionBusinessModel>();

        if (preferencesRequest.CustomerAttractionNavigation == null)
        {
            if (cityAttractions.Count > 5)
                topAttractions = cityAttractions.OrderBy(_ => Random.Next()).Take(5).ToList();
            else
                return cityAttractions.ToList();

            return topAttractions;
        }

        var reserveAttractions = new List<AttractionBusinessModel>();


        foreach (var attraction in cityAttractions)
        {
            if (preferencesRequest.CustomerAttractionNavigation.Architecture &&
                attraction.Kinds.Contains("architecture"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.Cultural && attraction.Kinds.Contains("cultural"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.Historical &&
                     attraction.Kinds.Contains("historic"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.Natural && attraction.Kinds.Contains("natural"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.Other && attraction.Kinds.Contains("other"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.Religion && attraction.Kinds.Contains("religion"))
                topAttractions.Add(attraction);

            else if (preferencesRequest.CustomerAttractionNavigation.IndustrialFacilities &&
                     attraction.Kinds.Contains("industrial"))
                topAttractions.Add(attraction);
            else
                reserveAttractions.Add(attraction);

            if (topAttractions.Count >= 5) return topAttractions;
            if (cityAttractions.Last().Xid == attraction.Xid)
                if (reserveAttractions.Count != 0)
                {
                    var topAttractionsNeedMore = 5 - topAttractions.Count;
                    topAttractions.AddRange(reserveAttractions.Count >= topAttractionsNeedMore
                        ? reserveAttractions.Take(topAttractionsNeedMore)
                        : reserveAttractions);
                }
        }

        return topAttractions;
    }

    private static HashSet<AttractionBusinessModel> FindAttractionsByCity(AgentLocalDb agentLocalDb, string city)
    {
        return agentLocalDb.AttractionsList.Where(a => a.Town == city).ToHashSet();
    }
}