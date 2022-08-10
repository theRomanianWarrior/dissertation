using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class AmenitiesPreferenceMapper
    {
        public static AmenitiesPreference ToEntity(this AmenitiesPreferenceDto amenitiesPreferenceDto)
        {
            return new AmenitiesPreference
            {
                Id = Guid.NewGuid(),
                AirConditioning = amenitiesPreferenceDto.AirConditioning,
                Dryer = amenitiesPreferenceDto.Dryer,
                Heating = amenitiesPreferenceDto.Heating,
                Iron = amenitiesPreferenceDto.Iron,
                Kitchen = amenitiesPreferenceDto.Kitchen,
                Tv = amenitiesPreferenceDto.Tv,
                Washer = amenitiesPreferenceDto.Washer,
                WiFi = amenitiesPreferenceDto.WiFi
            };
        }
    }
}
