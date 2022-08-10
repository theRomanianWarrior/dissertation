using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class PropertyTypePreferenceMapper
    {
        public static PropertyTypePreference ToEntity(this PropertyTypePreferenceDto propertyTypePreferenceDto)
        {
            return new PropertyTypePreference
            {
                Id = Guid.NewGuid(),
                Apartment = propertyTypePreferenceDto.Apartment,
                GuestHouse = propertyTypePreferenceDto.GuestHouse,
                Hotel = propertyTypePreferenceDto.Hotel,
                House = propertyTypePreferenceDto.House
            };
        }
    }
}
