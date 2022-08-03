using System.Net.Http.Headers;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.PropertyPreferencesResponse;
using VacationPackageWebApi.Domain.Property;
using static VacationPackageWebApi.Domain.Mas.BusinessLogic.CommonRecommendationLogic;

namespace VacationPackageWebApi.Domain.Mas.BusinessLogic
{
    public static class PropertyRecommendationLogic
    {
        private static readonly Random Random = new();

        public static bool FindOptimalPropertyAndStoreInMemory(Guid sourceAgentId,
            string initialAssignedAgentName, object recommendationPopulationLock, TourismAgent tourismAgent, PreferencesRequest preferencesRequest)
        {
            var cityProperties = FindPropertiesByCity(tourismAgent, preferencesRequest.DestinationCityNavigation.Name);
            
            if (cityProperties.Any())
            {
                var optimalProperty =
                    FindOptimalPropertyBasedOnUserPreferences(preferencesRequest, cityProperties);
                
                var propertyRecommendation = CreatePropertyRecommendationBModel(sourceAgentId, initialAssignedAgentName, optimalProperty);
                
                StoreInMemoryPropertyRecommendation(recommendationPopulationLock, propertyRecommendation);
                return true;
            }

            return false;
        }

        private static void StoreInMemoryPropertyRecommendation(object recommendationPopulationLock, PropertyRecommendationBModel propertyRecommendation)
        {
            var propertyPreferencesResponse = PopulatePropertyRecommendationResponse(propertyRecommendation);
            
            lock (recommendationPopulationLock)
            {
                (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!.PropertyPreferencesResponse ??= propertyPreferencesResponse;
            }
        }

        private static PropertyRecommendationResponse PopulatePropertyRecommendationResponse(PropertyRecommendationBModel propertyRecommendation)
        {
            return new PropertyRecommendationResponse
            {
                PropertyRecommendationBModel = propertyRecommendation
            };
        }
        private static PropertyRecommendationBModel CreatePropertyRecommendationBModel(Guid sourceAgentId, string initialAssignedAgentName, PropertyBusinessModel optimalProperty)
        {
            return new PropertyRecommendationBModel
            {
                SourceAgentId = sourceAgentId,
                InitialAssignedAgentName = initialAssignedAgentName,
                Property = optimalProperty,
                Status = "Up-to-date"
            };
        }

        private static PropertyBusinessModel FindOptimalPropertyBasedOnUserPreferences(PreferencesRequest preferencesRequest, HashSet<PropertyBusinessModel> properties)
        {
            var bestSimilarityRate = 0.0d;
            var randomPropertyIndex = Random.Next(properties.Count);
            var optimalProperty = properties.ElementAt(randomPropertyIndex);

            if (preferencesRequest.CustomerPropertyNavigation == null)
            {
                return optimalProperty;
            }
            
            foreach (var property in properties)
            {
                var currentSimilarityRate = CalculatePropertySimilarityRate(preferencesRequest, property);
                if (currentSimilarityRate > bestSimilarityRate)
                {
                    optimalProperty = property;
                    bestSimilarityRate = currentSimilarityRate;
                }
            }

            return optimalProperty;
        }

        private static double CalculatePropertySimilarityRate(PreferencesRequest preferencesRequest, PropertyBusinessModel property)
        {
            var propertyTypePreferenceRate = CheckPropertyTypePreferenceMatch(preferencesRequest.CustomerPropertyNavigation.PropertyTypeNavigation, property);
            var placeTypePreferenceRate = CheckPlaceTypePreferenceMatch(preferencesRequest.CustomerPropertyNavigation.PlaceTypeNavigation, property);
            
            var roomsAndBedsSimilarityRate = RoomsAndBedsSimilarityRate(preferencesRequest.CustomerPropertyNavigation.RoomsAndBedsNavigation, property);
            var amenitiesPreferenceRate = AmenitiesSimilarityRate(preferencesRequest.CustomerPropertyNavigation.AmenitiesNavigation, property);
            var petsPreferenceRate = preferencesRequest.CustomerPropertyNavigation.Pets == property.Pet ? 1 : 0;
            var combinedPreferencesRate = CalculateSimilarityRateBasedOnPreferenceRate(propertyTypePreferenceRate, placeTypePreferenceRate);
            return (combinedPreferencesRate + roomsAndBedsSimilarityRate + amenitiesPreferenceRate + petsPreferenceRate) / 4;
        }

        private static double CalculateSimilarityRateBasedOnPreferenceRate(bool propertyTypePreferenceRate, bool placeTypePreferenceRate)
        {
            return  ((propertyTypePreferenceRate
                       ? Match
                       : NoMatch) +
                   (double) (placeTypePreferenceRate 
                       ? Match 
                       : NoMatch)) / 2;
        }

        private static bool CheckPropertyTypePreferenceMatch(PropertyTypePreferenceDto? propertyTypePreference, PropertyBusinessModel property)
        {
            if (propertyTypePreference == null) return false;
            
            return propertyTypePreference.Apartment == (property.PropertyType.Id == (short) PropertyTypeId.Apartment) ||
                   propertyTypePreference.Hotel == (property.PropertyType.Id == (short) PropertyTypeId.Hotel) ||
                   propertyTypePreference.House == (property.PropertyType.Id == (short) PropertyTypeId.House) ||
                   propertyTypePreference.GuestHouse == (property.PropertyType.Id == (short) PropertyTypeId.GuestHouse);
        }
        
        private static bool CheckPlaceTypePreferenceMatch(PlaceTypePreferenceDto? placeTypePreference, PropertyBusinessModel property)
        {
            if (placeTypePreference == null) return false;

            return placeTypePreference.EntirePlace == (property.PlaceType.Id == (short) PlaceTypeId.EntirePlace) ||
                   placeTypePreference.PrivateRoom == (property.PlaceType.Id == (short) PlaceTypeId.PrivateRoom) ||
                   placeTypePreference.SharedRoom == (property.PlaceType.Id == (short) PlaceTypeId.SharedRoom);
        }

        private static double RoomsAndBedsSimilarityRate(RoomsAndBedsPreferenceDto? roomsAndBedsPreference, PropertyBusinessModel property)
        {
            if (roomsAndBedsPreference == null) return 0.0d;
            
            var bathroomPreferenceMatch = roomsAndBedsPreference.Bathrooms == property.RoomAndBed.Bathroom
                ? Match
                : NoMatch;
            
            var bedsPreferenceMatch = roomsAndBedsPreference.Beds == property.RoomAndBed.Bed
                ? Match
                : NoMatch;
            
            var bathRoomsPreferenceMatch = roomsAndBedsPreference.Bathrooms == property.RoomAndBed.Bathroom
                ? Match
                : NoMatch;

            return (double)(bathroomPreferenceMatch + bedsPreferenceMatch + bathRoomsPreferenceMatch) / 3;
        }

        private static double AmenitiesSimilarityRate(AmenitiesPreferenceDto? amenitiesPreference, PropertyBusinessModel property)
        {
            if (amenitiesPreference == null) return 0.0d;

            var wiFiPreferenceMatch = amenitiesPreference.WiFi == property.AmenitiesPackage.WiFi
                ? Match
                : NoMatch;
            
            var kitchenPreferenceMatch = amenitiesPreference.Kitchen == property.AmenitiesPackage.Kitchen
                ? Match
                : NoMatch;
            
            var washerPreferenceMatch = amenitiesPreference.Washer == property.AmenitiesPackage.Washer
                ? Match
                : NoMatch;
            
            var dryerPreferenceMatch = amenitiesPreference.Dryer == property.AmenitiesPackage.Dryer
                ? Match
                : NoMatch;
            
            var airConditioningPreferenceMatch = amenitiesPreference.AirConditioning == property.AmenitiesPackage.AirConditioning
                ? Match
                : NoMatch;
            
            var heatingPreferenceMatch = amenitiesPreference.Heating == property.AmenitiesPackage.Heating
                ? Match
                : NoMatch;
            
            var tvPreferenceMatch = amenitiesPreference.Tv == property.AmenitiesPackage.Tv
                ? Match
                : NoMatch;
            
            var ironPreferenceMatch = amenitiesPreference.Iron == property.AmenitiesPackage.Iron
                ? Match
                : NoMatch;
            
            return (double)(wiFiPreferenceMatch + kitchenPreferenceMatch + washerPreferenceMatch + dryerPreferenceMatch + 
                            airConditioningPreferenceMatch + heatingPreferenceMatch + tvPreferenceMatch + ironPreferenceMatch) / 8;
        }
        

        private static HashSet<PropertyBusinessModel> FindPropertiesByCity(AgentLocalDb agentLocalDb, string city)
        {
            return agentLocalDb.StaysList.Where(p => p.City.Name == city).ToHashSet();
        }
    }
}