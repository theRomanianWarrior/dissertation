namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class PreferencesPackage
    {
        public PreferencesPackage()
        {
            ClientRequests = new HashSet<ClientRequest>();
        }

        public Guid Id { get; set; }
        public Guid? CustomerFlight { get; set; }
        public Guid? CustomerProperty { get; set; }
        public Guid? CustomerAttraction { get; set; }
        public Guid PersonsByAge { get; set; }
        public Guid DepartureCity { get; set; }
        public Guid DestinationCity { get; set; }
        public DateOnly DepartureDate { get; set; }
        public short HolidaysPeriod { get; set; }

        public virtual AttractionPreference CustomerAttractionNavigation { get; set; }
        public virtual FlightDirectionPreference CustomerFlightNavigation { get; set; }
        public virtual PropertyPreference CustomerPropertyNavigation { get; set; }
        public virtual City DepartureCityNavigation { get; set; }
        public virtual City DestinationCityNavigation { get; set; }
        public virtual AgeCategoryPreference PersonsByAgeNavigation { get; set; }
        public virtual ICollection<ClientRequest> ClientRequests { get; set; }
    }
}
