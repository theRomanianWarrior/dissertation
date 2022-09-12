using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class FlightCompanyMapper
{
    public static FlightCompanyBusinessModel ToBusinessModel(this FlightCompany flightCompany)
    {
        return new FlightCompanyBusinessModel
        {
            Id = flightCompany.Id,
            Name = flightCompany.Name
        };
    }
}