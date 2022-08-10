using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class PropertyPreferenceMapper
    {
        public static PropertyPreference ToEntity(this PropertyPreferenceDto propertyPreferenceDto, Guid amenitiesPreferenceId, Guid placeTypePreferenceId, Guid propertyTypePreferenceId, Guid roomsAndBedsPreferenceId)
        {
            return new PropertyPreference
            {
                Id = Guid.NewGuid(),
                Pets = propertyPreferenceDto.Pets,
                Amenities = amenitiesPreferenceId,
                PlaceType = placeTypePreferenceId,
                PropertyType = propertyTypePreferenceId,
                RoomsAndBeds = roomsAndBedsPreferenceId
            };
        }
    }
}
