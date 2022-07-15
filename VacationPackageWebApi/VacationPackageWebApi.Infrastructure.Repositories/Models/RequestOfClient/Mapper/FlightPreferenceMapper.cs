using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper
{
    public static class FlightPreferenceMapper
    {
        public static FlightPreference? ToEntity(this FlightPreferenceDto? flightPreferenceDto, DeparturePeriodsPreference departurePeriodsPreference)
        {
            return new FlightPreference
            {
                Id = Guid.NewGuid(),
                DeparturePeriodPreferenceId = departurePeriodsPreference.Id,
                Stops = flightPreferenceDto!.StopsNavigation.Type
            };
        }
    }
}
