using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference;

public static class PlaceTypePreferenceMapper
{
    public static PlaceTypePreference ToEntity(this PlaceTypePreferenceDto placeTypePreferenceDto)
    {
        return new PlaceTypePreference
        {
            Id = Guid.NewGuid(),
            EntirePlace = placeTypePreferenceDto.EntirePlace,
            PrivateRoom = placeTypePreferenceDto.PrivateRoom,
            SharedRoom = placeTypePreferenceDto.SharedRoom
        };
    }
}