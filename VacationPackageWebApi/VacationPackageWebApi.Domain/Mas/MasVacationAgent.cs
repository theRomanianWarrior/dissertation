using ActressMas;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.Mas.Initializer;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;

namespace VacationPackageWebApi.Domain.Mas;

public class MasVacationAgent : Agent
{
    private static readonly object TaskDistributionLock = new();
    private static readonly object RecommendationPopulationLock = new();

    public TourismAgent TourismAgent;
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
                    ("Agent " + this.Name + " got request").WriteDebug();
                    Console.WriteLine("Agent " + this.Name + " got request");
                    _preferencesRequest = CommonRecommendationLogic.GetPreferencesPayload();

                    TourismAgent.ConfInd = CommonRecommendationLogic.GetCurrentAgentCustomizedExpertRate(TourismAgent.Id, _preferencesRequest.CustomizedExpertAgentRates);
                    
                    var taskType = CommonRecommendationLogic.AccessPreferencesAndChoseTask(TaskDistributionLock, TourismAgent);
                    ("Agent " + this.Name + " got task type " + taskType.ToString()).WriteDebug();
                    Console.WriteLine("Agent " + this.Name + " got task type " + taskType.ToString());
                    switch (taskType)
                    {
                        case TaskType.Default:
                            return;
                        case TaskType.Flight:
                        {
                            TourismAgent.Status = false;
                            TourismAgent.CurrentTask = TaskType.Flight;
                            List<string>? availableAgents = null;

                            FlightRecommendationLogic.FulfillFlightDefaultPreferencesWithCheapestOffer(
                                ref _preferencesRequest);

                            var optimalDepartureFlightSolutionStoredSuccess = 
                                FlightRecommendationLogic.FindOptimalDepartureFlightAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);

                            if (optimalDepartureFlightSolutionStoredSuccess == false)
                            {
                                TourismAgent.TrustGradeInOtherAgent = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation).ToList();

                                availableAgents = await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(TourismAgent);

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in TourismAgent.TrustGradeInOtherAgent)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        (Name + " send to " + trustRateInAgent.TrustedAgentName + " " +  "departure_flight_recommendation_request").WriteDebug();
                                        Send(trustRateInAgent.TrustedAgentName, "departure_flight_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                (Name + " send to Coordinator " + "departure_flight_recommendation_done").WriteDebug();
                                Send("Coordinator", "departure_flight_recommendation_done");
                            }

                            var optimalReturnFlightSolutionStoredSuccess =
                                FlightRecommendationLogic.FindOptimalReturnFlightAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);

                            if (optimalReturnFlightSolutionStoredSuccess == false)
                            {
                                if (optimalDepartureFlightSolutionStoredSuccess)
                                {
                                    availableAgents = await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(TourismAgent);

                                    if (availableAgents.Contains(TourismAgent.Name))
                                        availableAgents.Remove(TourismAgent.Name);
                                    
                                    if (!availableAgents.Any()) return; //// ??????????????????????????/
                                }
                                
                                TourismAgent.TrustGradeInOtherAgent = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation).ToList();

                                foreach (var trustRateInAgent in TourismAgent.TrustGradeInOtherAgent)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        (Name + " send to " + trustRateInAgent.TrustedAgentName  + " " +  "return_flight_recommendation_request").WriteDebug();
                                        
                                        Send(trustRateInAgent.TrustedAgentName, "return_flight_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                (Name + " send to Coordinator " + "return_flight_recommendation_done").WriteDebug();
                                
                                Send("Coordinator", "return_flight_recommendation_done");
                            }
                        } 
                            break;
                        case TaskType.Property:
                        {
                            TourismAgent.CurrentTask = TaskType.Property;
                            TourismAgent.Status = false;
                            
                            var optimalPropertySolutionStoredSuccess = PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            
                            if (optimalPropertySolutionStoredSuccess == false)
                            {
                                TourismAgent.TrustGradeInOtherAgent = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation).ToList();

                                var availableAgents = await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(TourismAgent);

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in TourismAgent.TrustGradeInOtherAgent)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        (Name + " send to " + trustRateInAgent.TrustedAgentName + " " + "property_recommendation_request").WriteDebug();
                                        
                                        Send(trustRateInAgent.TrustedAgentName, "property_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                (Name + " send to Coordinator " + "property_recommendation_done").WriteDebug();
                                
                                Send("Coordinator", "property_recommendation_done");
                            }
                        }
                            break;
                        case TaskType.Attractions:
                        {
                            TourismAgent.CurrentTask = TaskType.Attractions;
                            TourismAgent.Status = false;

                            var optimalAttractionSolutionStoredSuccess = AttractionsRecommendationLogic.FindOptimalAttractionAndStoreInMemory(TourismAgent.Id,
                                TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            
                            if (optimalAttractionSolutionStoredSuccess == false)
                            {
                                TourismAgent.TrustGradeInOtherAgent = CommonRecommendationLogic.GetAgentTrustRateOfAgentWithId(TourismAgent.Id)!.OrderByDescending(ta => ta.FlightTrust.PositiveEvaluation).ToList();

                                var availableAgents = await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(TourismAgent);

                                if (availableAgents.Contains(TourismAgent.Name))
                                    availableAgents.Remove(TourismAgent.Name);
                                
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                foreach (var trustRateInAgent in TourismAgent.TrustGradeInOtherAgent)
                                {
                                    if (availableAgents.Contains(trustRateInAgent.TrustedAgentName))
                                    {
                                        (Name + " send to Coordinator " + "attractions_recommendation_request").WriteDebug();
                                        
                                        Send(trustRateInAgent.TrustedAgentName, "attractions_recommendation_request");
                                    }
                                }
                            }
                            else
                            {
                                (Name + " send to Coordinator " + "attraction_recommendation_done").WriteDebug();
                                
                                Send("Coordinator", "attraction_recommendation_done");
                            }
                        }
                            break;
                    }
                    break;
                case "departure_flight_recommendation_request":
                {
                    TourismAgent.CurrentTask = TaskType.Flight;
                    TourismAgent.Status = false;

                    if (CommonRecommendationLogic.IsDepartureFlightRecommendationDone()) return;
                    
                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var departureFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalDepartureFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (departureFlightRecommendationSuccess)
                    {
                        (Name + " send to Coordinator " + "departure_flight_recommendation_done").WriteDebug();
                        
                        Send("Coordinator", "departure_flight_recommendation_done");
                    }

                }
                    break;
                case "return_flight_recommendation_request":
                {
                    TourismAgent.CurrentTask = TaskType.Flight;
                    TourismAgent.Status = false;

                    if (CommonRecommendationLogic.IsReturnFlightRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var returnFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalReturnFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (returnFlightRecommendationSuccess)
                    {
                        (Name + " send to Coordinator " + "return_flight_recommendation_done").WriteDebug();
                        
                        Send("Coordinator", "return_flight_recommendation_done");
                    }
                }
                    break;
                case "property_recommendation_request":
                {
                    TourismAgent.CurrentTask = TaskType.Property;
                    TourismAgent.Status = false;

                    if (CommonRecommendationLogic.IsPropertyRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var optimalPropertySolutionStoredSuccess =
                        PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalPropertySolutionStoredSuccess)
                    {
                        (Name + " send to Coordinator " + "property_recommendation_done").WriteDebug();
                        
                        Send("Coordinator", "property_recommendation_done");
                    }
                } 
                    break;
                case "attractions_recommendation_request":
                {
                    TourismAgent.CurrentTask = TaskType.Attractions;
                    TourismAgent.Status = false;

                    if (CommonRecommendationLogic.IsAttractionsRecommendationDone()) return;

                    CommonRecommendationLogic.RemoveAgentFromAvailableAgentsList(TourismAgent.Name);
                    var optimalAttractionSolutionStoredSuccess =
                        AttractionsRecommendationLogic.FindOptimalAttractionAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalAttractionSolutionStoredSuccess)
                    {
                        (Name + " send to Coordinator " + "attraction_recommendation_done").WriteDebug();
                        
                        Send("Coordinator", "attraction_recommendation_done");
                    }
                } 
                    break;
            }
            
            MasEnvVarsInitializer.ResetTourismAgent(ref TourismAgent);
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