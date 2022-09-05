using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Helpers.Models;
using VacationPackageWebApi.Domain.Mas.AgentsExpertBusinessModel;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;
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

        public void SaveEvaluation(ServiceEvaluationDto evaluationOfServices)
        {
            var departureFlightEvaluation = evaluationOfServices.FlightEvaluation.DepartureNavigation.ToEntity();
            var returnFlightEvaluation = evaluationOfServices.FlightEvaluation.ReturnNavigation.ToEntity();
            _context.FlightEvaluations.Add(departureFlightEvaluation);
            _context.FlightEvaluations.Add(returnFlightEvaluation);

            var propertyEvaluation = evaluationOfServices.PropertyEvaluation.ToEntity();
            _context.PropertyEvaluations.Add(propertyEvaluation);

            var allAttractionPoint = evaluationOfServices.AttractionEvaluation.ToEntity();
            _context.AllAttractionEvaluationPoints.Add(allAttractionPoint);

            _context.SaveChanges();
            
            var directionEvaluation = evaluationOfServices.FlightEvaluation.ToEntity(departureFlightEvaluation.Id, returnFlightEvaluation.Id);
            _context.FlightDirectionEvaluations.Add(directionEvaluation);

            foreach (var attractionsEvaluationEntity in evaluationOfServices.AttractionEvaluation.AttractionEvaluations.Select(attractionEvaluations => attractionEvaluations.ToEntity(allAttractionPoint.Id)))
            {
                _context.AttractionEvaluations.Add(attractionsEvaluationEntity);
            }
            
            _context.SaveChanges();

            var serviceEvaluation =
                evaluationOfServices.ToEntity(directionEvaluation.Id, propertyEvaluation.Id, allAttractionPoint.Id);
            _context.ServiceEvaluations.Add(serviceEvaluation);
            
            _context.SaveChanges();

            var clientRequest = _context.ClientRequests.SingleOrDefault(r => r.Id == evaluationOfServices.ClientRequestId);
            clientRequest!.Evaluation = serviceEvaluation.Id;
            _context.Entry(clientRequest).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public async Task<Task> UpdateAgentsSelfExpertRate()
        {
            // 29 and not 30 because count begin from 0( if current day is the same as day of evaluation we got 0, but we will begin count with 1)
            var some30DaysAgo = DateTime.Now.AddDays(-29);

            var agents = await _context.Agents.ToListAsync();
            foreach (var agent in agents)
            {
                var flightRatings = await GetAgentFlightRatings(agent.Id, some30DaysAgo);
                var attractionsRatings = await GetAgentAttractionsRatings(agent.Id, some30DaysAgo);
                var propertiesRatings = await GetAgentPropertyRatings(agent.Id, some30DaysAgo);

                if (flightRatings != null && flightRatings.Any())
                {
                    var agentFlightSelfExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(flightRatings, "Flight", agent.Name, false);
                    agent.FlightSelfExpertRate = agentFlightSelfExpertRate;
                }

                if (propertiesRatings != null && propertiesRatings.Any())
                {
                    var agentPropertySelfExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(propertiesRatings, "Property", agent.Name, false);
                    agent.PropertySelfExpertRate = agentPropertySelfExpertRate;
                }

                if (attractionsRatings != null && attractionsRatings.Any())
                {
                    var agentAttractionSelfExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(attractionsRatings, "Attractions", agent.Name, false);
                    agent.AttractionsSelfExpertRate = agentAttractionSelfExpertRate;
                }
                
                _context.Entry(agent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<Task> UpdateAgentTrustServiceEvaluation(ServiceEvaluationDto serviceEvaluation)
        {
            await UpdateTrustServiceEvaluationForDepartureFlight(serviceEvaluation);
            await UpdateTrustServiceEvaluationForReturnFlight(serviceEvaluation);
            await UpdateTrustServiceEvaluationForProperty(serviceEvaluation);
            await UpdateTrustServiceEvaluationForAttractionsPackage(serviceEvaluation);
            
            return Task.CompletedTask;
        }

        private async Task<List<AgentServiceRating>?> GetAgentFlightRatingsMatchCustomer(Guid agentId, Guid customerId, DateTime searchDataTillDate)
        {
            var agentDepartureFlightRecommendationsForCurrentCustomer = _context.ClientRequests.Any(c =>
                c.CustomerId == customerId &&
                c.Recommendation.FlightRecommendation.DepartureNavigation
                    .SourceAgentId == agentId &&
                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0))
                ? await _context.ClientRequests.Include(ce => ce.EvaluationNavigation).ThenInclude(ef => ef.FlightEvaluation).ThenInclude(efd => efd.DepartureNavigation)
                    .Where(c => c.Evaluation != null &&
                                c.CustomerId == customerId &&
                                c.Recommendation.FlightRecommendation.DepartureNavigation
                                    .SourceAgentId == agentId &&
                                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0)).Select(c => new AgentServiceRating()
                {
                    AgentId = agentId,
                    ServiceEvaluationDate = c.RequestTimestamp,
                    ServiceRating = c.EvaluationNavigation.FlightEvaluation.DepartureNavigation.FlightRating
                }).ToListAsync()
                : null;
            
            var agentReturnFlightRecommendationsForCurrentCustomer = _context.ClientRequests.Any(c =>
                c.CustomerId == customerId &&
                c.Recommendation.FlightRecommendation.ReturnNavigation
                    .SourceAgentId == agentId &&
                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0))
                ? await _context.ClientRequests.Include(ce => ce.EvaluationNavigation).ThenInclude(ef => ef.FlightEvaluation).ThenInclude(efd => efd.ReturnNavigation)
                    .Where(c => c.Evaluation != null &&
                                c.CustomerId == customerId &&
                                c.Recommendation.FlightRecommendation.ReturnNavigation
                                    .SourceAgentId == agentId &&
                                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0)).Select(c => new AgentServiceRating()
                {
                    AgentId = agentId,
                    ServiceEvaluationDate = c.RequestTimestamp,
                    ServiceRating = c.EvaluationNavigation.FlightEvaluation.ReturnNavigation.FlightRating
                }).ToListAsync()
                : null;

            if (agentDepartureFlightRecommendationsForCurrentCustomer == null)
                return agentReturnFlightRecommendationsForCurrentCustomer;
            
            if(agentReturnFlightRecommendationsForCurrentCustomer != null)
                agentDepartureFlightRecommendationsForCurrentCustomer.AddRange(agentReturnFlightRecommendationsForCurrentCustomer);
            
            return agentDepartureFlightRecommendationsForCurrentCustomer;
        }

        private async Task<List<AgentServiceRating>?> GetAgentPropertyRatingsMatchCustomer(Guid agentId, Guid customerId,
            DateTime searchDataTillDate)
        {
            var agentPropertyRecommendationsForCurrentCustomer = _context.ClientRequests.Any(c =>
                c.CustomerId == customerId &&
                c.Recommendation.PropertyRecommendation
                    .SourceAgentId == agentId &&
                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0))
                ? await _context.ClientRequests.Include(ce => ce.EvaluationNavigation).ThenInclude(ep => ep.PropertyEvaluation)
                    .Where(c => c.Evaluation != null &&
                                c.CustomerId == customerId &&
                                c.Recommendation.PropertyRecommendation
                                    .SourceAgentId == agentId &&
                                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0)).Select(c => new AgentServiceRating
                {
                    AgentId = agentId,
                    ServiceEvaluationDate = c.RequestTimestamp,
                    ServiceRating = c.EvaluationNavigation.PropertyEvaluation.FinalPropertyRating
                }).ToListAsync()
                : null;
            
            return agentPropertyRecommendationsForCurrentCustomer;
        }

        private async Task<List<AgentServiceRating>?> GetAgentAttractionsRatingsMatchCustomer(Guid agentId, Guid customerId,
            DateTime searchDataTillDate)
        {
           var agentAttractionPackagesRecommendationsForCurrentCustomer = _context.ClientRequests.Any(c =>
                c.CustomerId == customerId &&
                c.Recommendation.AttractionRecommendation
                    .SourceAgentId == agentId &&
                (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                 DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0))
                ? await _context.ClientRequests.Include(ce => ce.EvaluationNavigation).ThenInclude(ea => ea.AttractionEvaluation)
                    .Where(c => c.Evaluation != null &&
                    c.CustomerId == customerId &&
                    c.Recommendation.AttractionRecommendation
                        .SourceAgentId == agentId &&
                    (DateTime.Compare(c.RequestTimestamp, searchDataTillDate) > 0 ||
                     DateTime.Compare(c.RequestTimestamp, searchDataTillDate) == 0)).Select(c => new AgentServiceRating
                {
                    AgentId = agentId,
                    ServiceEvaluationDate = c.RequestTimestamp,
                    ServiceRating = c.EvaluationNavigation.AttractionEvaluation.FinalAttractionEvaluation
                }).ToListAsync()
                : null;
            
            return agentAttractionPackagesRecommendationsForCurrentCustomer;
        }
        
        public async Task UpdateCustomerPersonalAgentRate(ServiceEvaluationDto evaluationOfServices)
        {
            var customerId = _context.ClientRequests.Single(c => c.Id == evaluationOfServices.ClientRequestId)
                .CustomerId;

            // 29 and not 30 because count begin from 0( if current day is the same as day of evaluation we got 0, but we will begin count with 1)
            var some30DaysAgo = DateTime.Now.AddDays(-29);
            var personalAgentRateLogList = new List<PersonalAgentRateLogModel>();
            var agents = await _context.Agents.ToListAsync();
            var personalAgentServiceScores = new List<PersonalAgentServiceScoreLogModel>();
            
            foreach (var agent in agents)
            {
                var customerPersonalAgentRateRecordCreated = false;
                
                var flightRatingsMatchCustomer = await GetAgentFlightRatingsMatchCustomer(agent.Id, customerId, some30DaysAgo);
                var attractionsRatingsMatchCustomer = await GetAgentPropertyRatingsMatchCustomer(agent.Id, customerId, some30DaysAgo);
                var propertiesRatingsMatchCustomer = await GetAgentAttractionsRatingsMatchCustomer(agent.Id, customerId, some30DaysAgo);
                
                var customerPersonalAgentRate =
                    _context.CustomerPersonalAgentRates.SingleOrDefault(r =>
                        r.AgentId == agent.Id && r.CustomerId == customerId);
                
                if (customerPersonalAgentRate == null)
                {
                    customerPersonalAgentRate = new CustomerPersonalAgentRate
                    {
                        Id = Guid.NewGuid(),
                        AgentId = agent.Id,
                        CustomerId = customerId
                    };
                    
                    customerPersonalAgentRateRecordCreated = true;
                }

                if (flightRatingsMatchCustomer != null && flightRatingsMatchCustomer.Any())
                {
                    var customerPersonalAgentFlightExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(flightRatingsMatchCustomer, "Flight", agent.Name);
                    customerPersonalAgentRate.FlightExpertRate = customerPersonalAgentFlightExpertRate;
                }

                if (propertiesRatingsMatchCustomer != null && propertiesRatingsMatchCustomer.Any())
                {
                    var agentPropertySelfExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(propertiesRatingsMatchCustomer, "Property", agent.Name);
                    customerPersonalAgentRate.PropertyExpertRate = agentPropertySelfExpertRate;
                }

                if (attractionsRatingsMatchCustomer != null && attractionsRatingsMatchCustomer.Any())
                {
                    var agentAttractionSelfExpertRate = CalculateAgentExpertRateBasedOnTimeRelevance(attractionsRatingsMatchCustomer, "Attractions", agent.Name);
                    customerPersonalAgentRate.AttractionsExpertRate = agentAttractionSelfExpertRate;
                }

                if (!customerPersonalAgentRateRecordCreated)
                {
                    _context.Entry(customerPersonalAgentRate).State = EntityState.Modified;
                }
                else
                {
                    await _context.CustomerPersonalAgentRates.AddAsync(customerPersonalAgentRate);
                }
                
                personalAgentRateLogList.Add( new PersonalAgentRateLogModel()
                {
                    AgentName = agent.Name,
                    AttractionsExpertRate = customerPersonalAgentRate.AttractionsExpertRate,
                    FlightExpertRate = customerPersonalAgentRate.FlightExpertRate,
                    PropertyExpertRate = customerPersonalAgentRate.PropertyExpertRate
                });
                
                var personalAgentServiceScoreLogModel = new PersonalAgentServiceScoreLogModel()
                {
                    AgentName = agent.Name,
                    FlightRecommendationsDoneForCurrentUser = flightRatingsMatchCustomer?.Count ?? 0,
                    PropertyRecommendationsDoneForCurrentUser = propertiesRatingsMatchCustomer?.Count ?? 0,
                    AttractionsRecommendationsDoneForCurrentUser = attractionsRatingsMatchCustomer?.Count ?? 0
                };
                personalAgentServiceScores.Add(personalAgentServiceScoreLogModel);
            }

            foreach (var personalAgentServiceScore in personalAgentServiceScores)
            {
                UserReportHelper.WriteCustomerPersonalAgentServiceRecommendationsCounter(personalAgentServiceScore);
            }

            UserReportHelper.WriteCustomerPersonalAgentRate(personalAgentRateLogList);
            await _context.SaveChangesAsync();
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

        public Task WritePreferencesResponse(PreferencesResponse preferencesResponse)
        {
            var departureFlightSourceAgentName = _context.Agents.Single(a =>
                a.Id == preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation
                    .DepartureFlightRecommendation.SourceAgentId).Name;
            
            var returnFlightSourceAgentName = _context.Agents.Single(a =>
                a.Id == preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation
                    .ReturnFlightRecommendation.SourceAgentId).Name;
            
            var attractionsSourceAgentName = _context.Agents.Single(a =>
                a.Id == preferencesResponse.AttractionsRecommendationResponse.SourceAgentId).Name;
            
            var propertySourceAgentName = _context.Agents.Single(a =>
                a.Id == preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.SourceAgentId).Name;
            
            UserReportHelper.WritePreferencesResponse(preferencesResponse, departureFlightSourceAgentName, returnFlightSourceAgentName, propertySourceAgentName, attractionsSourceAgentName);

            return Task.CompletedTask;
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
        
        private float CalculateEvaluationBasedOnDate(float evaluationRate, int daysFromCurrentDay)
        {
            const float relevanceConstant = 0.16f;
            // relevance rate is a value between 0 and 1 depending on days from current day
            var relevanceRate = (float)
                Math.Pow(Math.E, -relevanceConstant * daysFromCurrentDay) / (float)Math.Pow(Math.E, -relevanceConstant);


            return (float) Math.Round(evaluationRate * relevanceRate, 2, MidpointRounding.ToZero);
        }
        
        private float CalculateAgentExpertRateBasedOnTimeRelevance(List<AgentServiceRating> agentServiceRatings,
                                                string serviceType, string agentName, bool isForSelfExpertRate = true) // added only for log
        {
            var evaluationsTotal = 0.0f;
            foreach (var agentServiceRating in agentServiceRatings)
            {
                //this gives a number from 1 to 30
                var daysPassedTillTodaySinceEvaluation = (int)(DateTime.Today.Date - agentServiceRating.ServiceEvaluationDate.Date).TotalDays + 1;
                var evaluationBasedOnDate = CalculateEvaluationBasedOnDate(agentServiceRating.ServiceRating, daysPassedTillTodaySinceEvaluation);
                /* This code is only for log */
                if (isForSelfExpertRate)
                {
                    var personalAgentSelfExpertRates = new PersonalAgentSelfExpertRateLogModel
                    {
                        AgentName = agentName,
                        DateOfRequest = agentServiceRating.ServiceEvaluationDate.Date.ToString("yyyy-MM-dd"),
                        DaysDifferenceFromToday = daysPassedTillTodaySinceEvaluation,
                        ServiceType = serviceType,
                        ExpertServiceRatings = new SelfExpertRatePropertiesLogModel
                        {
                            OriginalValue = agentServiceRating.ServiceRating,
                            ActualValue = evaluationBasedOnDate
                        }
                    };

                    UserReportHelper.WriteAgentsUpdatedSelfExpertRate(personalAgentSelfExpertRates);
                }

                /* Only for log */
                evaluationsTotal += evaluationBasedOnDate;
                
            }

            return evaluationsTotal / agentServiceRatings.Count;
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
                    var flightCompany = _context.FlightCompanies.SingleOrDefault(c =>
                        c.Name == departureFlightCompaniesPreference.Company.Name);
                    if (flightCompany != null)
                    {
                        var departureFlightCompanyPreference =
                            FlightCompaniesPreferenceMapper.ToEntity(flightCompany.Id, departureFlightPreference!.Id);
                        await _context.FlightCompaniesPreferences.AddAsync(departureFlightCompanyPreference);
                    }
                }

            if (preferencesPayload.CustomerFlightNavigation is {ReturnNavigation: {FlightCompaniesNavigationList: { }}})
            {
                foreach (var returnFlightCompaniesPreference in preferencesPayload.CustomerFlightNavigation
                             .ReturnNavigation.FlightCompaniesNavigationList)
                {
                    var flightCompany =
                        _context.FlightCompanies.SingleOrDefault(c =>
                            c.Name == returnFlightCompaniesPreference.Company.Name);
                    if (flightCompany != null)
                    {
                        var returnFlightCompanyPreference =
                            FlightCompaniesPreferenceMapper.ToEntity(flightCompany.Id, returnFlightPreference!.Id);
                        await _context.FlightCompaniesPreferences.AddAsync(returnFlightCompanyPreference);
                    }
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
        
        private async Task<List<AgentServiceRating>?> GetAgentFlightRatings(Guid agentId, DateTime daysAgoFromToday)
        {
            var flightReturnEvaluations = _context.ClientRequests.Any(cr =>
                cr.Recommendation.FlightRecommendation.ReturnNavigation.SourceAgentId.Equals(agentId) &&
                (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                 DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                ? _context.ClientRequests.Include(e => e.EvaluationNavigation).ThenInclude(fe => fe.FlightEvaluation).
                    ThenInclude(fer => fer.ReturnNavigation)
                    .Where(cr => cr.Evaluation != null &&
                    cr.Recommendation.FlightRecommendation.ReturnNavigation.SourceAgentId.Equals(agentId) &&
                    (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                     DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0)).ToList().Select(c => new AgentServiceRating()
                {
                    AgentId = agentId,
                    ServiceRating = c.EvaluationNavigation.FlightEvaluation.ReturnNavigation.FlightRating,
                    ServiceEvaluationDate = c.RequestTimestamp
                }).ToList()
                : null;

            var flightDepartureEvaluations = _context.ClientRequests.Any(cr =>
                cr.Recommendation.FlightRecommendation.DepartureNavigation.SourceAgentId.Equals(agentId) &&
                (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                 DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                ? _context.ClientRequests.Include(e => e.EvaluationNavigation).ThenInclude(fe => fe.FlightEvaluation).
                    ThenInclude(fer => fer.DepartureNavigation)
                    .Where(cr => cr.Evaluation != null &&
                                 cr.Recommendation.FlightRecommendation.DepartureNavigation.SourceAgentId.Equals(agentId) &&
                                 (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                                  DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0)).ToList().Select(c => new AgentServiceRating()
                    {
                        AgentId = agentId,
                        ServiceRating = c.EvaluationNavigation.FlightEvaluation.DepartureNavigation.FlightRating,
                        ServiceEvaluationDate = c.RequestTimestamp
                    }).ToList()
                : null;

            if (flightReturnEvaluations == null)
                return flightDepartureEvaluations;
            
            if(flightDepartureEvaluations != null)
                flightReturnEvaluations.AddRange(flightDepartureEvaluations);
            
            return flightReturnEvaluations;
        }

        private async Task<List<AgentServiceRating>?> GetAgentPropertyRatings(Guid agentId, DateTime daysAgoFromToday)
        {
            return _context.ClientRequests.Any(cr =>
                cr.Recommendation.PropertyRecommendation.SourceAgentId.Equals(agentId) &&
                (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                 DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                ? await _context.ClientRequests.Include(e => e.EvaluationNavigation).ThenInclude(pe => pe.PropertyEvaluation)
                    .Where(cr => cr.Evaluation != null &&
                        cr.Recommendation.PropertyRecommendation.SourceAgentId.Equals(agentId) &&
                        (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                         DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                    .Select(c => new AgentServiceRating()
                    {
                        AgentId = agentId,
                        ServiceRating = c.EvaluationNavigation.PropertyEvaluation.FinalPropertyRating,
                        ServiceEvaluationDate = c.RequestTimestamp
                    }).ToListAsync()
                : null;
        }
        
        private async Task<List<AgentServiceRating>?> GetAgentAttractionsRatings(Guid agentId, DateTime daysAgoFromToday)
        {
            return _context.ClientRequests.Any(cr =>
                cr.Recommendation.AttractionRecommendation.SourceAgentId.Equals(agentId) &&
                (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                 DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                ? await _context.ClientRequests.Include(e => e.EvaluationNavigation).ThenInclude(ae => ae.AttractionEvaluation)
                    .Where(cr => cr.Evaluation != null &&
                                 cr.Recommendation.AttractionRecommendation.SourceAgentId.Equals(agentId) &&
                                 (DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) > 0 ||
                                  DateTime.Compare(cr.RequestTimestamp, daysAgoFromToday) == 0))
                    .Select(c => new AgentServiceRating()
                    {
                        AgentId = agentId,
                        ServiceRating = c.EvaluationNavigation.AttractionEvaluation.FinalAttractionEvaluation,
                        ServiceEvaluationDate = c.RequestTimestamp
                    }).ToListAsync()
                : null;
        }
                private async Task UpdateTrustServiceEvaluationForAttractionsPackage(ServiceEvaluationDto serviceEvaluation)
        {
            var attractionsRecommendation =
                _context.ClientRequests.Include(r => r.Recommendation).ThenInclude(a => a.AttractionRecommendation) 
                    .Single(c => c.Id.Equals(serviceEvaluation.ClientRequestId)).Recommendation.AttractionRecommendation;


            var attractionsInitialAssignedAgentId = attractionsRecommendation.InitialAssignedAgentId;
            var attractionsSourceAgentId = attractionsRecommendation.SourceAgentId;

            if (attractionsInitialAssignedAgentId.Equals(attractionsSourceAgentId)) return;
            {
                var trustedAgent = _context.TrustedAgents.Include(ta => ta.TrustedAgentRateNavigation).ThenInclude(a => a.AttractionsTrustNavigation)
                    .Single(a =>
                        a.AgentId.Equals(attractionsInitialAssignedAgentId) &&
                        a.TrustedAgentId.Equals(attractionsSourceAgentId));

                if (serviceEvaluation.AttractionEvaluation.FinalAttractionEvaluation >= 0.5)
                {
                    trustedAgent.TrustedAgentRateNavigation.AttractionsTrustNavigation.PositiveEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.AttractionsTrustNavigation.LastPositiveEvaluation = DateTime.Now;
                }
                else
                {
                    trustedAgent.TrustedAgentRateNavigation.AttractionsTrustNavigation.NegativeEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.AttractionsTrustNavigation.LastNegativeEvaluation = DateTime.Now;
                }
                _context.Entry(trustedAgent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        private async Task UpdateTrustServiceEvaluationForProperty(ServiceEvaluationDto serviceEvaluation)
        {
            var propertyRecommendation =
                _context.ClientRequests.Include(r => r.Recommendation).ThenInclude(p => p.PropertyRecommendation) 
                    .Single(c => c.Id.Equals(serviceEvaluation.ClientRequestId)).Recommendation.PropertyRecommendation;


            var propertyInitialAssignedAgentId = propertyRecommendation.InitialAssignedAgentId;
            var propertySourceAgentId = propertyRecommendation.SourceAgentId;

            if (propertyInitialAssignedAgentId.Equals(propertySourceAgentId)) return;
            {
                var trustedAgent = _context.TrustedAgents.Include(ta => ta.TrustedAgentRateNavigation).ThenInclude(p => p.PropertyTrustNavigation)
                    .Single(a =>
                        a.AgentId.Equals(propertyInitialAssignedAgentId) &&
                        a.TrustedAgentId.Equals(propertySourceAgentId));

                if (serviceEvaluation.PropertyEvaluation.FinalPropertyRating >= 0.5)
                {
                    trustedAgent.TrustedAgentRateNavigation.PropertyTrustNavigation.PositiveEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.PropertyTrustNavigation.LastPositiveEvaluation = DateTime.Now;
                }
                else
                {
                    trustedAgent.TrustedAgentRateNavigation.PropertyTrustNavigation.NegativeEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.PropertyTrustNavigation.LastNegativeEvaluation = DateTime.Now;
                }
                _context.Entry(trustedAgent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        
        private async Task UpdateTrustServiceEvaluationForReturnFlight(ServiceEvaluationDto serviceEvaluation)
        {
            var flightRecommendation =
                _context.ClientRequests.Include(r => r.Recommendation).ThenInclude(f => f.FlightRecommendation).ThenInclude(fd => fd.ReturnNavigation)
                    .Single(c => c.Id.Equals(serviceEvaluation.ClientRequestId)).Recommendation.FlightRecommendation.ReturnNavigation;


            var returnFlightInitialAssignedAgentId = flightRecommendation.InitialAssignedAgentId;
            var returnFlightSourceAgentId = flightRecommendation.SourceAgentId;

            if (returnFlightInitialAssignedAgentId.Equals(returnFlightSourceAgentId)) return;
            {
                var trustedAgent = _context.TrustedAgents.Include(ta => ta.TrustedAgentRateNavigation).ThenInclude(f => f.FlightTrustNavigation)
                    .Single(a =>
                        a.AgentId.Equals(returnFlightInitialAssignedAgentId) &&
                        a.TrustedAgentId.Equals(returnFlightSourceAgentId));

                if (serviceEvaluation.FlightEvaluation.ReturnNavigation.FlightRating >= 0.5)
                {
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.PositiveEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.LastPositiveEvaluation = DateTime.Now;
                }
                else
                {
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.NegativeEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.LastNegativeEvaluation = DateTime.Now;
                }
                _context.Entry(trustedAgent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        private async Task UpdateTrustServiceEvaluationForDepartureFlight(ServiceEvaluationDto serviceEvaluation)
        {
            var flightRecommendation =
                _context.ClientRequests.Include(r => r.Recommendation).ThenInclude(f => f.FlightRecommendation).ThenInclude(fd => fd.DepartureNavigation)
                    .Single(c => c.Id.Equals(serviceEvaluation.ClientRequestId)).Recommendation.FlightRecommendation.DepartureNavigation;


            var departureFlightInitialAssignedAgentId = flightRecommendation.InitialAssignedAgentId;
            var departureFlightSourceAgentId = flightRecommendation.SourceAgentId;

            if (departureFlightInitialAssignedAgentId.Equals(departureFlightSourceAgentId)) return;
            {
                var trustedAgent = _context.TrustedAgents.Include(ta => ta.TrustedAgentRateNavigation).ThenInclude(f => f.FlightTrustNavigation)
                    .Single(a =>
                        a.AgentId.Equals(departureFlightInitialAssignedAgentId) &&
                        a.TrustedAgentId.Equals(departureFlightSourceAgentId));

                if (serviceEvaluation.FlightEvaluation.DepartureNavigation.FlightRating >= 0.5)
                {
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.PositiveEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.LastPositiveEvaluation = DateTime.Now;
                }
                else
                {
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.NegativeEvaluation += 1;
                    trustedAgent.TrustedAgentRateNavigation.FlightTrustNavigation.LastNegativeEvaluation = DateTime.Now;
                }
                _context.Entry(trustedAgent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
