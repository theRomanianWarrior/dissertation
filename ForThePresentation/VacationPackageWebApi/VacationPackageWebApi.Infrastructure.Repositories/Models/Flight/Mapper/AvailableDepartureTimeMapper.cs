using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class AvailableDepartureTimeMapper
{
    public static AvailableDepartureTimeBusinessModel ToBusinessModel(
        this AvailableDepartureTime availableDepartureTime)
    {
        return new AvailableDepartureTimeBusinessModel
        {
            Id = availableDepartureTime.Id,
            DepartureHour = availableDepartureTime.DepartureHour
        };
    }
}