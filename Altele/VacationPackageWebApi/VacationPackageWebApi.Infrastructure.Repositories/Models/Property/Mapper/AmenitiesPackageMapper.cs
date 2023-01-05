using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

public static class AmenitiesPackageMapper
{
    public static AmenitiesPackageBusinessModel ToBusinessModel(this AmenitiesPackage amenitiesPackage)
    {
        return new AmenitiesPackageBusinessModel
        {
            Id = amenitiesPackage.Id,
            AirConditioning = amenitiesPackage.AirConditioning,
            Dryer = amenitiesPackage.Dryer,
            Heating = amenitiesPackage.Heating,
            Iron = amenitiesPackage.Iron,
            Kitchen = amenitiesPackage.Kitchen,
            Tv = amenitiesPackage.Tv,
            Washer = amenitiesPackage.Washer,
            WiFi = amenitiesPackage.WiFi
        };
    }
}