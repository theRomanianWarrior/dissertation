using VacationPackageWebApi.Infrastructure.Repositories.Models;

namespace VacationPackageWebApi.Domain.PreferencesPackageRequest;

public record FlightCompaniesPreferenceDto
{
    public FlightCompanyDto Company { get; set; }
}