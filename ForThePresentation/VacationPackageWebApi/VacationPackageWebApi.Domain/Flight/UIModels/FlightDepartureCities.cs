namespace VacationPackageWebApi.Domain.Flight.UIModels;

public record FlightDepartureCities
{
    public string DepartureCityName { get; set; }
    public string DepartureCountryName { get; set; }
    public List<FlightDestinationCities>? FlightDestinationCitiesList { get; set; }
}