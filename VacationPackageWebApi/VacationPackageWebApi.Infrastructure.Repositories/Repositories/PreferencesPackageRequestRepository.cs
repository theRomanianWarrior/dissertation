using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories
{
    public class PreferencesPackageRequestRepository : IPreferencesPackageRequestRepository
    {
        private readonly VacationPackageContext _context;

        public PreferencesPackageRequestRepository(VacationPackageContext context)
        {
            _context = context;
        }

        public async Task<Guid> SavePreferences(PreferencesRequest preferencesPayload)
        {
            var preferencesPackageId = await ComposePreferencesPackageAsync(preferencesPayload);
            return preferencesPackageId;
        }

        public async Task<Task> SaveEvaluation(ServiceEvaluationDto evaluationOfServices)
        {
            var departureFlightEvaluation = evaluationOfServices.FlightEvaluation.DepartureNavigation.ToEntity();
            var returnFlightEvaluation = evaluationOfServices.FlightEvaluation.ReturnNavigation.ToEntity();
            await _context.FlightEvaluations.AddAsync(departureFlightEvaluation);
            await _context.FlightEvaluations.AddAsync(returnFlightEvaluation);

            var propertyEvaluation = evaluationOfServices.PropertyEvaluation.ToEntity();
            await _context.PropertyEvaluations.AddAsync(propertyEvaluation);

            var allAttractionPoint = evaluationOfServices.AttractionEvaluation.ToEntity();
            await _context.AllAttractionEvaluationPoints.AddAsync(allAttractionPoint);

            await _context.SaveChangesAsync();
            
            var directionEvaluation = evaluationOfServices.FlightEvaluation.ToEntity(departureFlightEvaluation.Id, returnFlightEvaluation.Id);
            await _context.FlightDirectionEvaluations.AddAsync(directionEvaluation);

            foreach (var attractionsEvaluationEntity in evaluationOfServices.AttractionEvaluation.AttractionEvaluations.Select(attractionEvaluations => attractionEvaluations.ToEntity(allAttractionPoint.Id)))
            {
                await _context.AttractionEvaluations.AddAsync(attractionsEvaluationEntity);
            }
            
            await _context.SaveChangesAsync();

            var serviceEvaluation =
                evaluationOfServices.ToEntity(directionEvaluation.Id, propertyEvaluation.Id, allAttractionPoint.Id);
            await _context.ServiceEvaluations.AddAsync(serviceEvaluation);
            
            await _context.SaveChangesAsync();

            var clientRequest = _context.ClientRequests.SingleOrDefault(r => r.Id == evaluationOfServices.ClientRequestId);
            clientRequest!.Evaluation = serviceEvaluation.Id;
            _context.Entry(clientRequest).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Task.CompletedTask;
        }
        
        public Action SaveRecommendation(PreferencesResponse preferencesResponse, Guid clientRequestId)
        {
            var flightDepartureRecommendationInitialAssignedAgentId = _context.Agents.SingleOrDefault(a =>
                a.Name == preferencesResponse.FlightRecommendationResponse!.FlightDirectionRecommendation!
                    .DepartureFlightRecommendation!.InitialAssignedAgentName)!.Id;
            
            var flightDepartureRecommendation = preferencesResponse.FlightRecommendationResponse!
                .FlightDirectionRecommendation!.DepartureFlightRecommendation!.ToEntity(flightDepartureRecommendationInitialAssignedAgentId);
            _context.FlightRecommendations.Add(flightDepartureRecommendation);
            
            var flightReturnRecommendationInitialAssignedAgentId = _context.Agents.SingleOrDefault(a =>
                a.Name == preferencesResponse.FlightRecommendationResponse!.FlightDirectionRecommendation!
                    .ReturnFlightRecommendation!.InitialAssignedAgentName)!.Id;
            
            var flightReturnRecommendation = preferencesResponse.FlightRecommendationResponse!
                .FlightDirectionRecommendation!.ReturnFlightRecommendation!.ToEntity(flightReturnRecommendationInitialAssignedAgentId);
            _context.FlightRecommendations.Add(flightReturnRecommendation);
            
            var propertyRecommendationInitialAssignedAgentId = _context.Agents.SingleOrDefault(a =>
                a.Name == preferencesResponse.PropertyPreferencesResponse!.PropertyRecommendationBModel.InitialAssignedAgentName)!.Id;
            var propertyRecommendation =
                preferencesResponse.PropertyPreferencesResponse!.PropertyRecommendationBModel.ToEntity(propertyRecommendationInitialAssignedAgentId);
            _context.PropertyRecommendations.Add(propertyRecommendation);

            var attractionsRecommendationInitialAssignedAgentId = _context.Agents.SingleOrDefault(a =>
                a.Name == preferencesResponse.AttractionsRecommendationResponse!.InitialAssignedAgentName)!.Id;
            var allAttractionsPackage = AllAttractionRecommendationMapper.ToEntity(
                preferencesResponse.AttractionsRecommendationResponse!.SourceAgentId,
                attractionsRecommendationInitialAssignedAgentId);
            _context.AllAttractionRecommendations.Add(allAttractionsPackage);
            _context.SaveChanges();

            
            var departureFlightConnections = preferencesResponse.FlightRecommendationResponse!.FlightDirectionRecommendation!
                                                                                    .DepartureFlightRecommendation!.FlightConnection.Select(departureFlightConnection => departureFlightConnection
                                                                                        .Flight.ToEntity(flightDepartureRecommendation.Id)).ToList();
            foreach (var departureFlightConnection in departureFlightConnections)
            {
                _context.FlightConnections.Add(departureFlightConnection);
            }
            
            var returnFlightConnections = preferencesResponse.FlightRecommendationResponse!.FlightDirectionRecommendation!
                .ReturnFlightRecommendation!.FlightConnection.Select(returnFlightConnection => returnFlightConnection
                    .Flight.ToEntity(flightReturnRecommendation.Id)).ToList();

            foreach (var returnFlightConnection in returnFlightConnections)
            {
                _context.FlightConnections.Add(returnFlightConnection);
            }
            
            var flightDirectionRecommendation =
                preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ToEntity(flightDepartureRecommendation.Id, flightReturnRecommendation.Id);
            _context.FlightDirectionRecommendations.Add(flightDirectionRecommendation);
            
            foreach (var attraction in preferencesResponse.AttractionsRecommendationResponse.AttractionRecommendationList)
            {
                var attractionRecommendation = attraction.Attraction.ToEntity(allAttractionsPackage.Id);
                _context.AttractionRecommendations.Add(attractionRecommendation);
            }

            _context.SaveChanges();

            var recommendation = RecommendationMapper.ToEntity(flightDirectionRecommendation.Id, propertyRecommendation.Id, allAttractionsPackage.Id);
            _context.Recommendations.Add(recommendation);
            _context.SaveChanges();

            AddRecommendationToExistingClientRequest(clientRequestId, recommendation.Id);
            _context.SaveChanges();
            Console.WriteLine("Recommendation saved in database.");
            return delegate {  };
        }

        public async Task<Task> CreateClientRequest(Guid customerId, Guid preferencesPackageId, Guid clientRequestId, DateTime requestTimestamp)
        {
            var clientRequest = new ClientRequest
            {
                Id = clientRequestId,
                PreferencesPackageId = preferencesPackageId,
                CustomerId = customerId,
                RequestTimestamp = requestTimestamp
            };

            await _context.ClientRequests.AddAsync(clientRequest); 
            await _context.SaveChangesAsync();
            
            return Task.CompletedTask;
        }

        private void AddRecommendationToExistingClientRequest(Guid clientRequestId, Guid recommendationId)
        {
            var clientRequest = _context.ClientRequests.SingleOrDefault(c => c.Id == clientRequestId);
            if (clientRequest != null)
            {
                clientRequest.RecommendationId = recommendationId;
                _context.Entry(clientRequest).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        
        private async Task<Guid> ComposePreferencesPackageAsync(PreferencesRequest preferencesPayload)
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

                return preferencesPackage.Id;
        }
    }
}
