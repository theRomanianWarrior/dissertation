using ActressMas;
using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Domain.Mas;

public class MasVacationAgent : Agent
{
    public TourismAgent TourismAgent;
    
    public MasVacationAgent(TourismAgent _tourismAgent)
    {
        TourismAgent = _tourismAgent;
    }
    
    public override void Setup()
    {
    }

    public override void Act(Message message)
    {
    }

    public override void ActDefault()
    {
        
    }
}