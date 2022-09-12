namespace VacationPackageWebApi.Domain.PreferencesPackageRequest;

public record DeparturePeriodsPreferenceDto
{
    public bool EarlyMorning { get; set; }
    public bool Morning { get; set; }
    public bool Afternoon { get; set; }
    public bool Night { get; set; }
}