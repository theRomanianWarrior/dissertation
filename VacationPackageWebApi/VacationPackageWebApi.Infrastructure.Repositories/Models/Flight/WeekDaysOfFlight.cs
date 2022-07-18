namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class WeekDaysOfFlight
    {
        public WeekDaysOfFlight()
        {
            Flights = new HashSet<Flight>();
        }

        public Guid Id { get; set; }
        public string DaysList { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
