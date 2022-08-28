﻿using ActressMas;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.Mas;

public class MasVacationAgent : Agent
{
    private static readonly object TaskDistributionLock = new();
    private static readonly object RecommendationPopulationLock = new();

    public readonly TourismAgent TourismAgent;
    private PreferencesRequest _preferencesRequest;

    public MasVacationAgent(TourismAgent tourismAgent)
    {
        TourismAgent = tourismAgent;
        _preferencesRequest = new PreferencesRequest();
    }
    
    public override void Setup()
    {
    }

    public override async void Act(Message message)
    {
        try
        {
            Console.WriteLine($"\t{message.Format()}");
            message.Parse(out var action, out string _);
            switch (action)
            {
                case "new_recommendation_request":
                    Console.WriteLine("Agent " + this.Name + " got request");
                    _preferencesRequest = CommonRecommendationLogic.GetPreferencesPayload();

                    var customizedExpertRate = CommonRecommendationLogic.GetCurrentAgentCustomizedExpertRate(TourismAgent.Id, _preferencesRequest.CustomizedExpertAgentRates);
                    
                    var taskType = CommonRecommendationLogic.AccessPreferencesAndChoseTask(TaskDistributionLock, TourismAgent, customizedExpertRate);
                    Console.WriteLine("Agent " + this.Name + " got task type " + taskType.ToString());
                    switch (taskType)
                    {
                        case TaskType.Default:
                            return;
                        case TaskType.Flight:
                        {
                            List<string>? availableAgents = null;

                            FlightRecommendationLogic.FulfillFlightDefaultPreferencesWithCheapestOffer(
                                ref _preferencesRequest);

                            var optimalDepartureFlightSolutionStoredSuccess = 
                                FlightRecommendationLogic.FindOptimalDepartureFlightAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);

                            if (optimalDepartureFlightSolutionStoredSuccess == false)
                            {
                                var trustRateInOtherAgents = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation);

                                availableAgents = await CommonRecommendationLogic.GetListOfAvailableAgentsAsync();

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in trustRateInOtherAgents)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        Send(trustRateInAgent.TrustedAgentName, "departure_flight_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                Send("Coordinator", "departure_flight_recommendation_done");
                            }

                            var optimalReturnFlightSolutionStoredSuccess =
                                FlightRecommendationLogic.FindOptimalReturnFlightAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);

                            if (optimalReturnFlightSolutionStoredSuccess == false)
                            {
                                if (optimalDepartureFlightSolutionStoredSuccess)
                                {
                                    availableAgents = await CommonRecommendationLogic.GetListOfAvailableAgentsAsync();

                                    if (availableAgents.Contains(TourismAgent.Name))
                                        availableAgents.Remove(TourismAgent.Name);
                                    
                                    if (!availableAgents.Any()) return; //// ??????????????????????????/
                                }
                                
                                var trustRateInOtherAgents = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation);

                                foreach (var trustRateInAgent in trustRateInOtherAgents)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        Send(trustRateInAgent.TrustedAgentName, "return_flight_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                Send("Coordinator", "return_flight_recommendation_done");
                            }
                        } 
                            break;
                        case TaskType.Property:
                        {
                            var optimalPropertySolutionStoredSuccess = PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            
                            if (optimalPropertySolutionStoredSuccess == false)
                            {
                                var trustRateInOtherAgents = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation);

                                var availableAgents = await CommonRecommendationLogic.GetListOfAvailableAgentsAsync();

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in trustRateInOtherAgents)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        Send(trustRateInAgent.TrustedAgentName, "property_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                Send("Coordinator", "property_recommendation_done");
                            }
                        }
                            break;
                        case TaskType.Attractions:
                        {
                            var optimalAttractionSolutionStoredSuccess = AttractionsRecommendationLogic.FindOptimalAttractionAndStoreInMemory(TourismAgent.Id,
                                TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            
                            if (optimalAttractionSolutionStoredSuccess == false)
                            {
                                var trustRateInOtherAgents = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation);

                                var availableAgents = await CommonRecommendationLogic.GetListOfAvailableAgentsAsync();

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in trustRateInOtherAgents)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        Send(trustRateInAgent.TrustedAgentName, "attractions_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                Send("Coordinator", "attraction_recommendation_done");
                            }
                        }
                            break;
                    }
                    break;
                case "departure_flight_recommendation_request":
                {
                    if (CommonRecommendationLogic.IsDepartureFlightRecommendationDone()) return;
                    
                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var departureFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalDepartureFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (departureFlightRecommendationSuccess)
                        Send("Coordinator", "departure_flight_recommendation_done");
                    
                }
                    break;
                case "return_flight_recommendation_request":
                {
                    if (CommonRecommendationLogic.IsReturnFlightRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var returnFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalReturnFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (returnFlightRecommendationSuccess)
                        Send("Coordinator", "return_flight_recommendation_done");
                }
                    break;
                case "property_recommendation_request":
                {
                    if (CommonRecommendationLogic.IsPropertyRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var optimalPropertySolutionStoredSuccess =
                        PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalPropertySolutionStoredSuccess)
                        Send("Coordinator", "property_recommendation_done");
                } 
                    break;
                case "attractions_recommendation_request":
                {
                    if (CommonRecommendationLogic.IsAttractionsRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var optimalAttractionSolutionStoredSuccess =
                        AttractionsRecommendationLogic.FindOptimalAttractionAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalAttractionSolutionStoredSuccess)
                        Send("Coordinator", "attraction_recommendation_done");
                } 
                    break;
            }
            await CommonRecommendationLogic.InsertAgentNameToAvailableAgents(TourismAgent.Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine("FromMasVacationAgent " + ex.Message);
        }
    }

    public override void ActDefault()
    {
    }
    
}