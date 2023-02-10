using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models;

public class City
{
    public City()
    {
        Airports = new HashSet<Airport>();
        PreferencesPackageDepartureCityNavigations = new HashSet<PreferencesPackage>();
        PreferencesPackageDestinationCityNavigations = new HashSet<PreferencesPackage>();
        Properties = new HashSet<Property.Property>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }

    public virtual Country Country { get; set; }
    public virtual ICollection<Airport> Airports { get; set; }
    public virtual ICollection<PreferencesPackage> PreferencesPackageDepartureCityNavigations { get; set; }
    public virtual ICollection<PreferencesPackage> PreferencesPackageDestinationCityNavigations { get; set; }
    public virtual ICollection<Property.Property> Properties { get; set; }
}