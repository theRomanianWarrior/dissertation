using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

public static class PlaceTypeMapper
{
    public static PlaceTypeBusinessModel ToBusinessModel(this PlaceType placeType)
    {
        return new PlaceTypeBusinessModel
        {
            Id = placeType.Id,
            Type = placeType.Type
        };
    }
}