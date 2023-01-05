namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;

public class Airport
{
    public Airport()
    {
        FlightArrivalAirports = new HashSet<Flight>();
        FlightDepartureAirports = new HashSet<Flight>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }

    public virtual City City { get; set; }
    public virtual ICollection<Flight> FlightArrivalAirports { get; set; }
    public virtual ICollection<Flight> FlightDepartureAirports { get; set; }
}