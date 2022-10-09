using TestWebAPI.Enums;

namespace TestWebAPI.PreferencesPackageRequest
{
    public record PlaceTypePreferenceDto
    {
        public bool EntirePlace { get; set; }
        public bool PrivateRoom { get; set; }
        public bool SharedRoom { get; set; }
    }
}
