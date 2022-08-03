namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class DeparturePeriodsPreference
    {
        public DeparturePeriodsPreference()
        {
            FlightPreferences = new HashSet<FlightPreference>();
        }

        public Guid Id { get; set; }
        public bool EarlyMorning { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Night { get; set; }

        public virtual ICollection<FlightPreference> FlightPreferences { get; set; }
    }
}
