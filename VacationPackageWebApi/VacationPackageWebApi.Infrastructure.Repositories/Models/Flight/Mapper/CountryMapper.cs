using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight.Mapper;

public static class CountryMapper
{
    public static CountryBusinessModel ToBusinessModel(this Country country)
    {
        return new CountryBusinessModel()
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}