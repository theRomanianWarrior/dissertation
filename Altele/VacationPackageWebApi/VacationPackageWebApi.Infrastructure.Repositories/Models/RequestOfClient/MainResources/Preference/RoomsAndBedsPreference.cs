namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

public class RoomsAndBedsPreference
{
    public RoomsAndBedsPreference()
    {
        PropertyPreferences = new HashSet<PropertyPreference>();
    }

    public Guid Id { get; set; }
    public short Bedrooms { get; set; }
    public short Beds { get; set; }
    public short Bathrooms { get; set; }

    public virtual ICollection<PropertyPreference> PropertyPreferences { get; set; }
}