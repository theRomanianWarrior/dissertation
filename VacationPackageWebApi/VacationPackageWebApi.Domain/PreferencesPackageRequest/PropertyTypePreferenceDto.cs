using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.PreferencesPackageRequest
{
    public record PropertyTypePreferenceDto
    {
        public bool House { get; set; }
        public bool Apartment { get; set; }
        public bool GuestHouse { get; set; }
        public bool Hotel { get; set; }
    }
}
