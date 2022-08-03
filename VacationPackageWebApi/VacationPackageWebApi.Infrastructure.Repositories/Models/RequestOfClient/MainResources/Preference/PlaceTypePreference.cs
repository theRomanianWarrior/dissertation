namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class PlaceTypePreference
    {
        public PlaceTypePreference()
        {
            PropertyPreferences = new HashSet<PropertyPreference>();
        }

        public Guid Id { get; set; }
        public bool EntirePlace { get; set; }
        public bool PrivateRoom { get; set; }
        public bool SharedRoom { get; set; }

        public virtual ICollection<PropertyPreference> PropertyPreferences { get; set; }
    }
}
