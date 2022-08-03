using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class FlightCompaniesPreference
    {
        public Guid Id { get; set; }
        public Guid FlightPreferenceId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual FlightCompany Company { get; set; }
        public virtual FlightPreference FlightPreference { get; set; }
    }
}
