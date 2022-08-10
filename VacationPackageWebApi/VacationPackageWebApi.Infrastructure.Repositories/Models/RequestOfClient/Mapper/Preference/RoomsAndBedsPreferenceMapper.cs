using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class RoomsAndBedsPreferenceMapper
    {
        public static RoomsAndBedsPreference ToEntity(this RoomsAndBedsPreferenceDto roomsAndBedsPreferenceDto)
        {
            return new RoomsAndBedsPreference
            {
                Id = Guid.NewGuid(),
                Bathrooms = roomsAndBedsPreferenceDto.Bathrooms,
                Bedrooms = roomsAndBedsPreferenceDto.Bedrooms,
                Beds = roomsAndBedsPreferenceDto.Beds
            };
        }
    }
}
