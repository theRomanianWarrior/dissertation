using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class WeekDaysOfFlightMapper
{
    public static WeekDaysOfFlightBusinessModel ToBusinessModel(this WeekDaysOfFlight weekDaysOfFlight)
    {
        return new WeekDaysOfFlightBusinessModel
        {
            Id = weekDaysOfFlight.Id,
            DaysList = weekDaysOfFlight.DaysList
        };
    }
}