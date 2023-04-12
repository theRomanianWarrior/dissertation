using System;
using System.Collections.Generic;
using System.Linq;
using TestWebAPI.Attractions;
using TestWebAPI.PreferencesPackageRequest;
using TestWebAPI.PreferencesPackageResponse;
using TestWebAPI.PreferencesPackageResponse.AttractionsPreferencesResponse;

namespace TestWebAPI.BusinessLogic;

public static class AttractionsRecommendationLogic
{
    private static readonly Random Random = new();

    public static double CalculateAttractionsSimilarityRate(
        PreferencesRequest preferencesRequest, List<AttractionRecommendationBModel> cityAttractions)
    {
        var matchCounter = 0;

        if (!cityAttractions.Any())
        {
            return 0;
        }
            
        foreach (var attraction in cityAttractions)
        {
            if (preferencesRequest.CustomerAttractionNavigation!.Architecture &&
                attraction.Attraction.Kinds.Contains("architecture"))
            {
                ++matchCounter;
            }
            else if (preferencesRequest.CustomerAttractionNavigation.Cultural && 
                     attraction.Attraction.Kinds.Contains("cultural"))
            {
                ++matchCounter;
            }

            else if (preferencesRequest.CustomerAttractionNavigation.Historical &&
                     attraction.Attraction.Kinds.Contains("historic"))
            {
                ++matchCounter;
            }
            else if (preferencesRequest.CustomerAttractionNavigation.Natural &&
                     attraction.Attraction.Kinds.Contains("natural"))
            {
                ++matchCounter;
            }
            else if (preferencesRequest.CustomerAttractionNavigation.Other &&
                     attraction.Attraction.Kinds.Contains("other"))
            {
                ++matchCounter;
            }
            else if (preferencesRequest.CustomerAttractionNavigation.Religion &&
                     attraction.Attraction.Kinds.Contains("religion"))
            {
                ++matchCounter;
            }
            else if (preferencesRequest.CustomerAttractionNavigation.IndustrialFacilities &&
                     attraction.Attraction.Kinds.Contains("industrial"))
            {
                ++matchCounter;
            }
        }
        
        return (double)matchCounter / cityAttractions.Count;
    }
}