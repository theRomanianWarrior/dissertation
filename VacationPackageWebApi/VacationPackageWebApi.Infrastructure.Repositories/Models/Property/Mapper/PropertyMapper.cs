using VacationPackageWebApi.Domain.Property;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

public static class PropertyMapper
{
    public static PropertyBusinessModel ToBusinessModel(this Property property)
    {
        return new PropertyBusinessModel()
        {
            Id = property.Id,
            AmenitiesPackage = property.AmenitiesPackage.ToBusinessModel(),
            RoomAndBed = property.RoomAndBed.ToBusinessModel(),
            City = property.City.ToBusinessModel(),
            Name = property.Name,
            Pet = property.Pet,
            PlaceType = property.PlaceType.ToBusinessModel(),
            PropertyType = property.PropertyType.ToBusinessModel(),
        };
    }
}