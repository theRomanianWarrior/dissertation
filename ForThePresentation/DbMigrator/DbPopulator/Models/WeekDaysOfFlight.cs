namespace DbPopulator.Models
{
    public record WeekDaysOfFlight
    {
        public Guid Id { get; set; }
        public string DaysList { get; set; }
    }
}