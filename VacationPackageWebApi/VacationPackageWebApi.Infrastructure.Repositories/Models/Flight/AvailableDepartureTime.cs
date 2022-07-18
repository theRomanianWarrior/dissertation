namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class AvailableDepartureTime
    {
        public AvailableDepartureTime()
        {
            Flights = new HashSet<Flight>();
        }

        public Guid Id { get; set; }
        public string DepartureHour { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
