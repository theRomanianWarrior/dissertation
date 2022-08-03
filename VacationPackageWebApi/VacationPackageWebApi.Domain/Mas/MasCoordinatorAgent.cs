using ActressMas;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;

namespace VacationPackageWebApi.Domain.Mas;

public class MasCoordinatorAgent : Agent
{
    private const int TotalNumberOfServices = 4;
    private readonly List<CoordinatorTasksDone> _tasksDone = new();
    
    public override void Setup()
    {

    }

    public override void Act(Message message)
    {
        try
        {
            Console.WriteLine($"\t{message.Format()}");
            message.Parse(out var action, out string parameters);

            switch (action)
            {
                case "departure_flight_recommendation_done":
                    _tasksDone.Add(CoordinatorTasksDone.DepartureFlight);
                    break;
                case "return_flight_recommendation_done":
                    _tasksDone.Add(CoordinatorTasksDone.ReturnFlight);
                    break;
                case "property_recommendation_done":
                    _tasksDone.Add(CoordinatorTasksDone.Property);
                    break;
                case "attraction_recommendation_done":
                    _tasksDone.Add(CoordinatorTasksDone.Attraction);
                    break;
            }
            if(_tasksDone.Count == TotalNumberOfServices)
                CommonRecommendationLogic.SetPreferencesResponseStatusDone();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }  
    }
    
    public override void ActDefault()
    {
        
    }
}