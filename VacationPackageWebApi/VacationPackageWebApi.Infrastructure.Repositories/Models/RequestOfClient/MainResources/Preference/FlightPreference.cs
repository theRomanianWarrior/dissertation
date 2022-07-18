using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class FlightPreference
    {
        public FlightPreference()
        {
            FlightCompaniesPreferences = new HashSet<FlightCompaniesPreference>();
            FlightDirectionPreferenceDepartureNavigations = new HashSet<FlightDirectionPreference>();
            FlightDirectionPreferenceReturnNavigations = new HashSet<FlightDirectionPreference>();
        }

        public Guid Id { get; set; }
        public Guid DeparturePeriodPreferenceId { get; set; }
        public short Stops { get; set; }

        public virtual DeparturePeriodsPreference DeparturePeriodPreference { get; set; }
        public virtual StopsTypePreference StopsNavigation { get; set; }
        public virtual ICollection<FlightCompaniesPreference> FlightCompaniesPreferences { get; set; }
        public virtual ICollection<FlightDirectionPreference> FlightDirectionPreferenceDepartureNavigations { get; set; }
        public virtual ICollection<FlightDirectionPreference> FlightDirectionPreferenceReturnNavigations { get; set; }
    }
}
