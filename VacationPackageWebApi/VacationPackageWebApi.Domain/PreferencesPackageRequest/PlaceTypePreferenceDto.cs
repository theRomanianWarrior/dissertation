namespace VacationPackageWebApi.Domain.PreferencesPackageRequest
{
    public record PlaceTypePreferenceDto
    {
        public bool EntirePlace { get; set; }
        public bool PrivateRoom { get; set; }
        public bool SharedRoom { get; set; }
    }
}
