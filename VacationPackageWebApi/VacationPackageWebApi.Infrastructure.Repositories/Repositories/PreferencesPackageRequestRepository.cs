using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper;
using PreferencesPackage = VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference.PreferencesPackage;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories
{
    public class PreferencesPackageRequestRepository : IPreferencesPackageRequestRepository
    {
        private readonly VacationPackageContext _context;

        public PreferencesPackageRequestRepository(VacationPackageContext context)
        {
            _context = context;
        }

        public async Task<Task> SavePreferences(PreferencesRequest preferencesPayload)
        {
            await ComposePreferencesPackageAsync(preferencesPayload);
            return Task.CompletedTask;
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
            
            var departureFlightPreference = preferencesPayload.CustomerFlightNavigation.DepartureNavigation.ToEntity(departurePeriodsPreference);
            var returnFlightPreference = preferencesPayload.CustomerFlightNavigation.ReturnNavigation.ToEntity(departurePeriodsPreference);
            var propertyPreference = preferencesPayload.CustomerPropertyNavigation.ToEntity(amenitiesPreference.Id, placeTypePreference.Id, propertyTypePreference.Id, roomsAndBedsPreference.Id);

            await _context.FlightPreferences.AddAsync(departureFlightPreference!);
            await _context.FlightPreferences.AddAsync(returnFlightPreference!);
            await _context.PropertyPreferences.AddAsync(propertyPreference);

            await _context.SaveChangesAsync();

            var flightDirectionPreference = preferencesPayload.CustomerFlightNavigation.ToEntity(departureFlightPreference!, returnFlightPreference!);
            await _context.FlightDirectionPreferences.AddAsync(flightDirectionPreference);

            foreach (var flightCompanyPreferenceDto in preferencesPayload.FlightCompaniesNavigationList)
            {
                var flightCompanyId = _context.FlightCompanies.SingleOrDefault(c => c.Name == flightCompanyPreferenceDto.Company.Name)!.Id;

                switch (flightCompanyPreferenceDto.FlightDirection)
                {
                    case "Departure":
                    {
                        var departureFlightCompanyPreference = FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, departureFlightPreference!.Id);
                        await _context.FlightCompaniesPreferences.AddAsync(departureFlightCompanyPreference!);
                        break;
                    }
                    case "Return":
                    {
                        var returnFlightCompanyPreference = FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, returnFlightPreference!.Id);
                        await _context.FlightCompaniesPreferences.AddAsync(returnFlightCompanyPreference!);
                        break;
                    }
                }
            }

            await _context.SaveChangesAsync();

            var departureCityId = _context.Cities.SingleOrDefault(c => c.Name == preferencesPayload.DepartureCityNavigation.Name)!.Id;
            var destinationCityId = _context.Cities.SingleOrDefault(c => c.Name == preferencesPayload.DestinationCityNavigation.Name)!.Id;

            var preferencesPackage = preferencesPayload.ToEntity(attractionPreference.Id, flightDirectionPreference.Id, propertyPreference.Id, departureCityId, destinationCityId, ageCategoryPreference.Id);
            
            _context.PreferencesPackages.Add(preferencesPackage);
            await _context.SaveChangesAsync();

            return preferencesPackage;
        }
    }
}
