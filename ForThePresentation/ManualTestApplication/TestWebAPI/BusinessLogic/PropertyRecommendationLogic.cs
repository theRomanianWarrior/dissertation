using TestWebAPI.Enums;
using TestWebAPI.PreferencesPackageRequest;
using TestWebAPI.PreferencesPackageResponse.PropertyPreferencesResponse;

namespace TestWebAPI.BusinessLogic;

public static class PropertyRecommendationLogic
{
    private const int Match = 1;
    private const int NoMatch = 0;
    
    public static double CalculatePropertySimilarityRate(PreferencesRequest preferencesRequest,
        PropertyRecommendationResponse property)
    {
        var propertyTypePreferenceRate =
            CheckPropertyTypePreferenceMatch(preferencesRequest.CustomerPropertyNavigation!.PropertyTypeNavigation,
                property);
        var placeTypePreferenceRate =
            CheckPlaceTypePreferenceMatch(preferencesRequest.CustomerPropertyNavigation.PlaceTypeNavigation, property);

        var roomsAndBedsSimilarityRate =
            RoomsAndBedsSimilarityRate(preferencesRequest.CustomerPropertyNavigation.RoomsAndBedsNavigation, property);
        var amenitiesPreferenceRate =
            AmenitiesSimilarityRate(preferencesRequest.CustomerPropertyNavigation.AmenitiesNavigation, property);
        var petsPreferenceRate = preferencesRequest.CustomerPropertyNavigation.Pets == property.PropertyRecommendationBModel.Property.Pet ? 1 : 0;
        var combinedPreferencesRate =
            CalculateSimilarityRateBasedOnPreferenceRate(propertyTypePreferenceRate, placeTypePreferenceRate);
        return (combinedPreferencesRate + roomsAndBedsSimilarityRate + amenitiesPreferenceRate + petsPreferenceRate) /
               4;
    }

    private static double CalculateSimilarityRateBasedOnPreferenceRate(bool propertyTypePreferenceRate,
        bool placeTypePreferenceRate)
    {
        return ((propertyTypePreferenceRate
                    ? Match
                    : NoMatch) +
                (double) (placeTypePreferenceRate
                    ? Match
                    : NoMatch)) / 2;
    }

    private static bool CheckPropertyTypePreferenceMatch(PropertyTypePreferenceDto? propertyTypePreference,
        PropertyRecommendationResponse property)
    {
        if (propertyTypePreference == null) return false;

        return propertyTypePreference.Apartment == (property.PropertyRecommendationBModel.Property.PropertyType.Id == (short) PropertyTypeId.Apartment) ||
               propertyTypePreference.Hotel == (property.PropertyRecommendationBModel.Property.PropertyType.Id == (short) PropertyTypeId.Hotel) ||
               propertyTypePreference.House == (property.PropertyRecommendationBModel.Property.PropertyType.Id == (short) PropertyTypeId.House) ||
               propertyTypePreference.GuestHouse == (property.PropertyRecommendationBModel.Property.PropertyType.Id == (short) PropertyTypeId.GuestHouse);
    }

    private static bool CheckPlaceTypePreferenceMatch(PlaceTypePreferenceDto? placeTypePreference,
        PropertyRecommendationResponse property)
    {
        if (placeTypePreference == null) return false;

        return placeTypePreference.EntirePlace == (property.PropertyRecommendationBModel.Property.PlaceType.Id == (short) PlaceTypeId.EntirePlace) ||
               placeTypePreference.PrivateRoom == (property.PropertyRecommendationBModel.Property.PlaceType.Id == (short) PlaceTypeId.PrivateRoom) ||
               placeTypePreference.SharedRoom == (property.PropertyRecommendationBModel.Property.PlaceType.Id == (short) PlaceTypeId.SharedRoom);
    }

    private static double RoomsAndBedsSimilarityRate(RoomsAndBedsPreferenceDto? roomsAndBedsPreference,
        PropertyRecommendationResponse property)
    {
        if (roomsAndBedsPreference == null) return 0.0d;

        var bathroomPreferenceMatch = roomsAndBedsPreference.Bathrooms == property.PropertyRecommendationBModel.Property.RoomAndBed.Bathroom
            ? Match
            : NoMatch;

        var bedsPreferenceMatch = roomsAndBedsPreference.Beds == property.PropertyRecommendationBModel.Property.RoomAndBed.Bed
            ? Match
            : NoMatch;

        var bathRoomsPreferenceMatch = roomsAndBedsPreference.Bathrooms == property.PropertyRecommendationBModel.Property.RoomAndBed.Bathroom
            ? Match
            : NoMatch;

        return (double) (bathroomPreferenceMatch + bedsPreferenceMatch + bathRoomsPreferenceMatch) / 3;
    }

    private static double AmenitiesSimilarityRate(AmenitiesPreferenceDto? amenitiesPreference,
        PropertyRecommendationResponse property)
    {
        if (amenitiesPreference == null) return 0.0d;

        var wiFiPreferenceMatch = amenitiesPreference.WiFi == property.PropertyRecommendationBModel.Property.AmenitiesPackage.WiFi
            ? Match
            : NoMatch;

        var kitchenPreferenceMatch = amenitiesPreference.Kitchen == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Kitchen
            ? Match
            : NoMatch;

        var washerPreferenceMatch = amenitiesPreference.Washer == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Washer
            ? Match
            : NoMatch;

        var dryerPreferenceMatch = amenitiesPreference.Dryer == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Dryer
            ? Match
            : NoMatch;

        var airConditioningPreferenceMatch =
            amenitiesPreference.AirConditioning == property.PropertyRecommendationBModel.Property.AmenitiesPackage.AirConditioning
                ? Match
                : NoMatch;

        var heatingPreferenceMatch = amenitiesPreference.Heating == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Heating
            ? Match
            : NoMatch;

        var tvPreferenceMatch = amenitiesPreference.Tv == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Tv
            ? Match
            : NoMatch;

        var ironPreferenceMatch = amenitiesPreference.Iron == property.PropertyRecommendationBModel.Property.AmenitiesPackage.Iron
            ? Match
            : NoMatch;

        return (double) (wiFiPreferenceMatch + kitchenPreferenceMatch + washerPreferenceMatch + dryerPreferenceMatch +
                         airConditioningPreferenceMatch + heatingPreferenceMatch + tvPreferenceMatch +
                         ironPreferenceMatch) / 8;
    }
}