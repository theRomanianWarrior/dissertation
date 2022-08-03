using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class FlightClass
    {
        public FlightClass()
        {
            FlightPreferences = new HashSet<FlightPreference>();
            FlightPrices = new HashSet<FlightPrice>();
        }

        public short Id { get; set; }
        public string Class { get; set; }

        public virtual ICollection<FlightPreference> FlightPreferences { get; set; }
        public virtual ICollection<FlightPrice> FlightPrices { get; set; }
    }
}
