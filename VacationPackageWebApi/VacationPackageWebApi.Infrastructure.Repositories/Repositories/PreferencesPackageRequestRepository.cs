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
            await _context.AgeCategoryPreferences.AddAsync(ageCategoryPreference);

            DeparturePeriodsPreference? departurePeriodsPreference = null;
            AmenitiesPreference? amenitiesPreference = null;
            PlaceTypePreference? placeTypePreference = null;
            PropertyTypePreference? propertyTypePreference = null;
            RoomsAndBedsPreference? roomsAndBedsPreference = null;
            FlightPreference? departureFlightPreference = null;
            FlightPreference? returnFlightPreference = null;
            AttractionPreference? attractionPreference = null;
            FlightDirectionPreference? flightDirectionPreference = null;
            PropertyPreference? propertyPreference = null;
            
            if (preferencesPayload.CustomerFlightNavigation != null)
            {
                if (preferencesPayload.CustomerFlightNavigation.DepartureNavigation is {DeparturePeriodPreference: { }})
                {
                    departurePeriodsPreference = preferencesPayload.CustomerFlightNavigation.DepartureNavigation
                        .DeparturePeriodPreference.ToEntity();
                    await _context.DeparturePeriodsPreferences.AddAsync(departurePeriodsPreference);
                }
                
                if (preferencesPayload.CustomerFlightNavigation.ReturnNavigation is {DeparturePeriodPreference: { }})
                {
                    var returnPeriodsPreference = preferencesPayload.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference.ToEntity();
                    await _context.DeparturePeriodsPreferences.AddAsync(returnPeriodsPreference);
                }
            }

            if (preferencesPayload.CustomerPropertyNavigation is {PlaceTypeNavigation: { }})
            {
                placeTypePreference =
                    preferencesPayload.CustomerPropertyNavigation.PlaceTypeNavigation.ToEntity();
                await _context.PlaceTypePreferences.AddAsync(placeTypePreference);
                    
                if (preferencesPayload.CustomerPropertyNavigation.PropertyTypeNavigation != null)
                {
                    propertyTypePreference =
                        preferencesPayload.CustomerPropertyNavigation.PropertyTypeNavigation.ToEntity();
                    await _context.PropertyTypePreferences.AddAsync(propertyTypePreference);
                }

                if (preferencesPayload.CustomerPropertyNavigation.AmenitiesNavigation != null)
                {
                    amenitiesPreference = preferencesPayload.CustomerPropertyNavigation.AmenitiesNavigation.ToEntity();
                    await _context.AmenitiesPreferences.AddAsync(amenitiesPreference);
                }

                if (preferencesPayload.CustomerPropertyNavigation.RoomsAndBedsNavigation != null)
                {
                    roomsAndBedsPreference = preferencesPayload.CustomerPropertyNavigation.RoomsAndBedsNavigation.ToEntity();
                    await _context.RoomsAndBedsPreferences.AddAsync(roomsAndBedsPreference);
                }
            }

            if (preferencesPayload.CustomerAttractionNavigation != null)
            {
                attractionPreference = preferencesPayload.CustomerAttractionNavigation.ToEntity();
                await _context.AttractionPreferences.AddAsync(attractionPreference);
            }

            await _context.SaveChangesAsync();

            if (preferencesPayload.CustomerFlightNavigation != null)
            {
                if (departurePeriodsPreference != null)
                {
                    departureFlightPreference =
                        preferencesPayload.CustomerFlightNavigation.DepartureNavigation.ToEntity(
                            departurePeriodsPreference);
                    await _context.FlightPreferences.AddAsync(departureFlightPreference!);
                }
            }

            if (preferencesPayload.CustomerFlightNavigation != null)
            {
                if (departurePeriodsPreference != null)
                {
                    returnFlightPreference =
                        preferencesPayload.CustomerFlightNavigation.ReturnNavigation.ToEntity(
                            departurePeriodsPreference);
                    await _context.FlightPreferences.AddAsync(returnFlightPreference!);
                }
            }

            if (preferencesPayload.CustomerPropertyNavigation != null)
            {
                if (amenitiesPreference != null && placeTypePreference != null && propertyTypePreference != null && roomsAndBedsPreference != null)
                {
                    propertyPreference = preferencesPayload.CustomerPropertyNavigation.ToEntity(
                        amenitiesPreference.Id, placeTypePreference.Id, propertyTypePreference.Id,
                        roomsAndBedsPreference.Id);
                    await _context.PropertyPreferences.AddAsync(propertyPreference);
                }
            }

            await _context.SaveChangesAsync();

            if (departureFlightPreference != null && returnFlightPreference != null)
            {
                if (preferencesPayload.CustomerFlightNavigation != null)
                {
                    flightDirectionPreference =
                        preferencesPayload.CustomerFlightNavigation.ToEntity(departureFlightPreference,
                            returnFlightPreference);
                    await _context.FlightDirectionPreferences.AddAsync(flightDirectionPreference);
                }
            }

            if (preferencesPayload.CustomerFlightNavigation is {DepartureNavigation:
             {
                 FlightCompaniesNavigationList: { }
             }})
                foreach (var departureFlightCompaniesPreference in preferencesPayload.CustomerFlightNavigation
                             .DepartureNavigation.FlightCompaniesNavigationList)
                {
                    var flightCompanyId = _context.FlightCompanies.SingleOrDefault(c =>
                        c.Name == departureFlightCompaniesPreference.Company.Name)!.Id;
                    var departureFlightCompanyPreference =
                        FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, departureFlightPreference!.Id);
                    await _context.FlightCompaniesPreferences.AddAsync(departureFlightCompanyPreference);
                }

            if (preferencesPayload.CustomerFlightNavigation is {ReturnNavigation: {FlightCompaniesNavigationList: { }}})
            {
                foreach (var returnFlightCompaniesPreference in preferencesPayload.CustomerFlightNavigation
                             .ReturnNavigation.FlightCompaniesNavigationList)
                {
                    var flightCompanyId =
                        _context.FlightCompanies.SingleOrDefault(c =>
                            c.Name == returnFlightCompaniesPreference.Company.Name)!.Id;
                    var returnFlightCompanyPreference =
                        FlightCompaniesPreferenceMapper.ToEntity(flightCompanyId, returnFlightPreference!.Id);
                    await _context.FlightCompaniesPreferences.AddAsync(returnFlightCompanyPreference);
                }
            }

            await _context.SaveChangesAsync();

            var departureCityId = _context.Cities.SingleOrDefault(c => c.Name == preferencesPayload.DepartureCityNavigation.Name)!.Id;
            var destinationCityId = _context.Cities.SingleOrDefault(c => c.Name == preferencesPayload.DestinationCityNavigation.Name)!.Id;
            
                var preferencesPackage = preferencesPayload.ToEntity(attractionPreference?.Id,
                    flightDirectionPreference?.Id, propertyPreference?.Id, departureCityId, destinationCityId,
                    ageCategoryPreference.Id);
                _context.PreferencesPackages.Add(preferencesPackage);

                await _context.SaveChangesAsync();

            return preferencesPackage;
        }
    }
}
