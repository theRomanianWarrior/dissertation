using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories
{
    public class PreferencesPackageRequestRepository : IPreferencesPackageRequestRepository
    {
        private readonly VacationPackageContext _context;

        public PreferencesPackageRequestRepository(VacationPackageContext context)
        {
            _context = context;
        }

        public Task<PreferencesResponse> GetFullPackageRecommendations(PreferencesRequest preferencesPayload)
        {
            throw new NotImplementedException();
        }

        public Task SavePreferences(PreferencesRequest preferencesPayload)
        {
            throw new NotImplementedException();
        }

        private async Task<PreferencesPackage> ComposePreferencesPackageAsync(PreferencesRequest preferencesPayload)
        {
            var ageCategoryPreference = preferencesPayload.PersonsByAgeNavigation.ToEntity();

            var departurePeriodsPreference = preferencesPayload.CustomerFlightNavigation.DepartureNavigation.DeparturePeriodPreference.ToEntity();
            var returnPeriodsPreference = preferencesPayload.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference.ToEntity();

            var placeTypePreference = preferencesPayload.CustomerPropertyNavigation.PlaceTypeNavigation.ToEntity();
            var propertyTypePreference = preferencesPayload.CustomerPropertyNavigation.PropertyTypeNavigation.ToEntity();
            var amenitiesPreference = preferencesPayload.CustomerPropertyNavigation.AmenitiesNavigation.ToEntity();
            var roomsAndBedsPreference = preferencesPayload.CustomerPropertyNavigation.RoomsAndBedsNavigation.ToEntity();

            var attractionPreference = preferencesPayload.CustomerAttractionNavigation.ToEntity();

            await _context.AgeCategoryPreferences.AddAsync(ageCategoryPreference);

            await _context.DeparturePeriodsPreferences.AddAsync(departurePeriodsPreference);
            await _context.DeparturePeriodsPreferences.AddAsync(returnPeriodsPreference);

            await _context.PlaceTypePreferences.AddAsync(placeTypePreference);
            await _context.PropertyTypePreferences.AddAsync(propertyTypePreference);
            await _context.AmenitiesPreferences.AddAsync(amenitiesPreference);
            await _context.RoomsAndBedsPreferences.AddAsync(roomsAndBedsPreference);

            await _context.AttractionPreferences.AddAsync(attractionPreference);

            await _context.SaveChangesAsync();
            
            var departureFlightPreference = FlightPreferenceMapper.ToEntity(preferencesPayload.CustomerFlightNavigation.DepartureNavigation, departurePeriodsPreference);
            var returnFlightPreference = FlightPreferenceMapper.ToEntity(preferencesPayload.CustomerFlightNavigation.ReturnNavigation, departurePeriodsPreference);
            var propertyPreference = PropertyPreferenceMapper.ToEntity(preferencesPayload.CustomerPropertyNavigation, amenitiesPreference.Id, placeTypePreference.Id, propertyTypePreference.Id, roomsAndBedsPreference.Id);

            await _context.FlightPreferences.AddAsync(departureFlightPreference!);
            await _context.FlightPreferences.AddAsync(returnFlightPreference!);
            await _context.PropertyPreferences.AddAsync(propertyPreference);

            await _context.SaveChangesAsync();

            var flightDirectionPreference = FlightDirectionPreferenceMapper.ToEntity(preferencesPayload.CustomerFlightNavigation, departureFlightPreference!, returnFlightPreference!);
            await _context.FlightDirectionPreferences.AddAsync(flightDirectionPreference!);

            foreach (var flightCompanyPreferenceDto in preferencesPayload.FlightCompaniesNavigationList)
            {
                var flightCompanyId = _context.FlightCompanies.Where(c => c.Name == flightCompanyPreferenceDto.Company.Name).SingleOrDefault()!.Id;

                if (flightCompanyPreferenceDto.FlightDirection == "Departure")
                {
                    var departureFlightCompanyPreference = FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, departureFlightPreference!.Id);
                    await _context.FlightCompaniesPreferences.AddAsync(departureFlightCompanyPreference!);
                }

                if (flightCompanyPreferenceDto.FlightDirection == "Return")
                {
                    var returnFlightCompanyPreference = FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, returnFlightPreference!.Id);
                    await _context.FlightCompaniesPreferences.AddAsync(returnFlightCompanyPreference!);
                }
            }

            await _context.SaveChangesAsync();

            var departureCityId = _context.Cities.Where(c => c.Name == preferencesPayload.DepartureCityNavigation.Name).SingleOrDefault()!.Id;
            var destinationCityId = _context.Cities.Where(c => c.Name == preferencesPayload.DestinationCityNavigation.Name).SingleOrDefault()!.Id;

            var preferencesPackage = PreferencesPackageMapper.ToEntity(preferencesPayload, attractionPreference.Id, flightDirectionPreference.Id, propertyPreference.Id, departureCityId, destinationCityId, ageCategoryPreference.Id);
            
            _context.PreferencesPackages.Add(preferencesPackage);
            await _context.SaveChangesAsync();

            return preferencesPackage;
        }
    }
}
