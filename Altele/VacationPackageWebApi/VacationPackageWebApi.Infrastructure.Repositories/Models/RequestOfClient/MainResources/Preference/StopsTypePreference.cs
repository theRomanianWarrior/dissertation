namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

public class StopsTypePreference
{
    public StopsTypePreference()
    {
        FlightPreferences = new HashSet<FlightPreference>();
    }

    public short Id { get; set; }
    public string Type { get; set; }

    public virtual ICollection<FlightPreference> FlightPreferences { get; set; }
}