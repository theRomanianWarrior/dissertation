namespace VacationPackageWebApi.Domain.PreferencesPackageRequest
{
    public record PreferencesRequest
    {
        public DateTime DepartureDate { get; set; }
        public short HolidaysPeriod { get; set; }

        public AttractionPreferenceDto CustomerAttractionNavigation { get; set; }
        public FlightDirectionPreferenceDto CustomerFlightNavigation { get; set; }
        public PropertyPreferenceDto CustomerPropertyNavigation { get; set; }
        public CityDto DepartureCityNavigation { get; set; }
        public CityDto DestinationCityNavigation { get; set; }
        public AgeCategoryPreferenceDto PersonsByAgeNavigation { get; set; }
        public List<FlightCompaniesPreferenceDto> FlightCompaniesNavigationList { get; set; }
    }
}
