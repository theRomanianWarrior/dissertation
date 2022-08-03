namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class FlightDirectionPreference
    {
        public FlightDirectionPreference()
        {
            PreferencesPackages = new HashSet<PreferencesPackage>();
        }

        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }

        public virtual FlightPreference DepartureNavigation { get; set; }
        public virtual FlightPreference ReturnNavigation { get; set; }
        public virtual ICollection<PreferencesPackage> PreferencesPackages { get; set; }
    }
}
