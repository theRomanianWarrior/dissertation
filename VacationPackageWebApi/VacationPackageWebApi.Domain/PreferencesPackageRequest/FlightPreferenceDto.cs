namespace VacationPackageWebApi.Domain.PreferencesPackageRequest
{
    public record FlightPreferenceDto
    {
        public DeparturePeriodsPreferenceDto DeparturePeriodPreference { get; set; }
        public StopsTypePreferenceDto StopsNavigation { get; set; }
    }
}
