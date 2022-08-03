using ActressMas;
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

                    //can be null
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
                                availableAgents =
                                    await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(
                                        TourismAgent);
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                SendToMany(availableAgents, "departure_flight_recommendation_request");
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
                                    availableAgents =
                                        await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(
                                            TourismAgent);
                                    if (!availableAgents.Any()) return; //// ??????????????????????????/
                                }

                                SendToMany(availableAgents, "return_flight_recommendation_request");
                            }
                            else
                            {
                                Send("Coordinator", "return_flight_recommendation_done");
                            }
                        } 
                            break;
                        case TaskType.Property:
                        {
                            var optimalPropertySolutionStoredSuccess =
                                PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                                    TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            if (optimalPropertySolutionStoredSuccess == false)
                            {
                                var availableAgents =
                                    await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(
                                        TourismAgent);
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                SendToMany(availableAgents, "property_recommendation_request");
                            }
                            else
                            {
                                Send("Coordinator", "property_recommendation_done");
                            }
                        }
                            break;
                        case TaskType.Attractions:
                        {
                            var optimalAttractionSolutionStoredSuccess =
                            AttractionsRecommendationLogic.FindOptimalAttractionAndStoreInMemory(TourismAgent.Id,
                                TourismAgent.Name, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                            if (optimalAttractionSolutionStoredSuccess == false)
                            {
                                var availableAgents =
                                    await CommonRecommendationLogic.GetListOfAllAgentsExceptCurrentAndCoordinator(
                                        TourismAgent);
                                if (!availableAgents.Any()) return; //// ??????????????????????????/

                                SendToMany(availableAgents, "attractions_recommendation_request");
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
                    var departureFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalDepartureFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (departureFlightRecommendationSuccess)
                        Send("Coordinator", "departure_flight_recommendation_done");
                }
                    break;
                case "return_flight_recommendation_request":
                {
                    var returnFlightRecommendationSuccess =
                        FlightRecommendationLogic.FindOptimalReturnFlightAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (returnFlightRecommendationSuccess)
                        Send("Coordinator", "return_flight_recommendation_done");
                }
                    break;
                case "property_recommendation_request":
                {
                    var optimalPropertySolutionStoredSuccess =
                        PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalPropertySolutionStoredSuccess)
                        Send("Coordinator", "property_recommendation_done");
                } 
                    break;
                case "attractions_recommendation_request":
                {
                    var optimalAttractionSolutionStoredSuccess =
                        PropertyRecommendationLogic.FindOptimalPropertyAndStoreInMemory(TourismAgent.Id,
                            message.Sender, RecommendationPopulationLock, TourismAgent, _preferencesRequest);
                    if (optimalAttractionSolutionStoredSuccess)
                        Send("Coordinator", "attraction_recommendation_done");
                } 
                    break;
            }
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