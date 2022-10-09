using VacationPackageWebApi.Infrastructure.Repositories.Models;

namespace TestWebAPI.PreferencesPackageRequest
{
    public record FlightCompaniesPreferenceDto
    {
        public FlightCompanyDto Company { get; set; }
    }
}
