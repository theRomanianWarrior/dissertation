namespace VacationPackageWepApp.ServerModels.Flight
{
    public record WeekDaysOfFlightBusinessModel
    {
        public Guid Id { get; set; }
        public string DaysList { get; set; }
    }
}
