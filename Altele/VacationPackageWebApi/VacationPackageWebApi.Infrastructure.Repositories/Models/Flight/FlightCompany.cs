using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;

public class FlightCompany
{
    public FlightCompany()
    {
        FlightCompaniesPreferences = new HashSet<FlightCompaniesPreference>();
        Flights = new HashSet<Flight>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<FlightCompaniesPreference> FlightCompaniesPreferences { get; set; }
    public virtual ICollection<Flight> Flights { get; set; }
}