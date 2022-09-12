namespace VacationPackageWepApp.UiDataStoring.Preference;

public record PropertyPreferenceDto
{
    public bool Pets { get; set; }

    public AmenitiesPreferenceDto? AmenitiesNavigation { get; set; }
    public PlaceTypePreferenceDto? PlaceTypeNavigation { get; set; }
    public PropertyTypePreferenceDto? PropertyTypeNavigation { get; set; }
    public RoomsAndBedsPreferenceDto? RoomsAndBedsNavigation { get; set; }
}