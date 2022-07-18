using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class PropertyPreference
    {
        public PropertyPreference()
        {
            PreferencesPackages = new HashSet<PreferencesPackage>();
        }

        public Guid Id { get; set; }
        public Guid PropertyType { get; set; }
        public Guid PlaceType { get; set; }
        public Guid RoomsAndBeds { get; set; }
        public bool Pets { get; set; }
        public Guid Amenities { get; set; }

        public virtual AmenitiesPreference AmenitiesNavigation { get; set; }
        public virtual PlaceTypePreference PlaceTypeNavigation { get; set; }
        public virtual PropertyTypePreference PropertyTypeNavigation { get; set; }
        public virtual RoomsAndBedsPreference RoomsAndBedsNavigation { get; set; }
        public virtual ICollection<PreferencesPackage> PreferencesPackages { get; set; }
    }
}
