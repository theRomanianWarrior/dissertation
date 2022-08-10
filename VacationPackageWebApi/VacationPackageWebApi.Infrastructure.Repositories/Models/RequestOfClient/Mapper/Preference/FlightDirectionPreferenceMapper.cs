using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class FlightDirectionPreferenceMapper
    {
        public static FlightDirectionPreference ToEntity(this FlightDirectionPreferenceDto flightDirectionPreferenceDto, FlightPreference departureFlightPreference, FlightPreference returnFlightPreference)
        {
            return new FlightDirectionPreference
            {
                Id = Guid.NewGuid(),
                Departure = departureFlightPreference.Id,
                Return = returnFlightPreference.Id
            };
        }
    }
}
