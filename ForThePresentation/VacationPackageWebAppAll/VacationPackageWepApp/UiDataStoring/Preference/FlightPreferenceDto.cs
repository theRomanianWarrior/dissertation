namespace VacationPackageWepApp.UiDataStoring.Preference;

public record FlightPreferenceDto
{
    public DeparturePeriodsPreferenceDto? DeparturePeriodPreference { get; set; }
    public StopsTypePreferenceDto? StopsNavigation { get; set; }
    public FlightClassDto? Class { get; set; }
    public List<FlightCompaniesPreferenceDto>? FlightCompaniesNavigationList { get; set; }
}