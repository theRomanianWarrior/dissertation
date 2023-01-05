namespace VacationPackageWebApi.Domain.PreferencesPackageRequest;

public record RoomsAndBedsPreferenceDto
{
    public short Bedrooms { get; set; }
    public short Beds { get; set; }
    public short Bathrooms { get; set; }
}