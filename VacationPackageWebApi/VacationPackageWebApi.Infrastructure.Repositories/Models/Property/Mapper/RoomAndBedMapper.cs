using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

public static class RoomAndBedMapper
{
    public static RoomAndBedBusinessModel ToBusinessModel(this RoomAndBed roomAndBed)
    {
        return new RoomAndBedBusinessModel
        {
            Id = roomAndBed.Id,
            Bathroom = roomAndBed.Bathroom,
            Bed = roomAndBed.Bed,
            Bedroom = roomAndBed.Bedroom
        };
    }
}