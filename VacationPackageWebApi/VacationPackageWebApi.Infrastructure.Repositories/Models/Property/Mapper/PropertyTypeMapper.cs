using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

public static class PropertyTypeMapper
{
    public static PropertyTypeBusinessModel ToBusinessModel(this PropertyType propertyType)
    {
        return new PropertyTypeBusinessModel()
        {
            Id = propertyType.Id,
            Type = propertyType.Type
        };
    }
}