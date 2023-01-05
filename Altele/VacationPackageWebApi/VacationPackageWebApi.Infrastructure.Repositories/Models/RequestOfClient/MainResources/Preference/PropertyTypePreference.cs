namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

public class PropertyTypePreference
{
    public PropertyTypePreference()
    {
        PropertyPreferences = new HashSet<PropertyPreference>();
    }

    public Guid Id { get; set; }
    public bool House { get; set; }
    public bool Apartment { get; set; }
    public bool GuestHouse { get; set; }
    public bool Hotel { get; set; }

    public virtual ICollection<PropertyPreference> PropertyPreferences { get; set; }
}