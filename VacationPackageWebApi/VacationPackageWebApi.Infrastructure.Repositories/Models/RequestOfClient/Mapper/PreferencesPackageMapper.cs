using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper
{
    public static class PreferencesPackageMapper
    {
        public static PreferencesPackage ToEntity(this PreferencesRequest preferencesRequest, Guid? customerAttractionId, Guid? customerFlightId, Guid? customerPropertyId, Guid departureCityId, Guid destinationCityId, Guid personsByAgeId)
        {
            return new PreferencesPackage
            {
                Id = Guid.NewGuid(),
                DepartureDate = DateOnly.FromDateTime(preferencesRequest.DepartureDate),
                HolidaysPeriod = preferencesRequest.HolidaysPeriod,
                CustomerAttraction = customerAttractionId,
                CustomerFlight = customerFlightId,
                CustomerProperty = customerPropertyId,
                DepartureCity = departureCityId,
                DestinationCity = destinationCityId,
                PersonsByAge = personsByAgeId,
            };
        }
    }
}
