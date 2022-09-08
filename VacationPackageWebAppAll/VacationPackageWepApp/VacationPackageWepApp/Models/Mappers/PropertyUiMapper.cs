using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;

namespace VacationPackageWepApp.Models.Mappers;

public static class PropertyUiMapper
{
    public static PropertyUiModel ToPropertyUiModel(this PreferencesResponse preferencesResponse)
    {
        return new PropertyUiModel
        {
            PropertyName = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.Name,
            Pets = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.Pet
                ? "yes"
                : "no",
            City = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.City.Name,
            PlaceType = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.PlaceType
                .Type,
            PropertyType = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                .PropertyType.Type,
            Bedroom = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed
                .Bedroom.ToString(),
            Bed = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed.Bed
                .ToString(),
            Bathroom = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed
                .Bathroom.ToString(),
            Amenities = new AmenitiesUiModel()
            {
                WiFi = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.WiFi
                    ? "yes"
                    : "no",
                Kitchen = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Kitchen
                    ? "yes"
                    : "no",
                Washer = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Washer
                    ? "yes"
                    : "no",
                Dryer = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Dryer
                    ? "yes"
                    : "no",
                AirConditioning =
                    preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                        .AmenitiesPackage.AirConditioning
                        ? "yes"
                        : "no",
                Heating = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Heating
                    ? "yes"
                    : "no",
                Tv = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Tv
                    ? "yes"
                    : "no",
                Iron = preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property
                    .AmenitiesPackage.Iron
                    ? "yes"
                    : "no"
            }
        };
    }
}