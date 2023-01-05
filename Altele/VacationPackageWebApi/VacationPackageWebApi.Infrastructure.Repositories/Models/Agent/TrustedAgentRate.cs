namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent;

public class TrustedAgentRate
{
    public TrustedAgentRate()
    {
        TrustedAgents = new HashSet<TrustedAgent>();
    }

    public Guid Id { get; set; }
    public Guid FlightTrust { get; set; }
    public Guid PropertyTrust { get; set; }
    public Guid AttractionsTrust { get; set; }

    public virtual TrustServiceEvaluation AttractionsTrustNavigation { get; set; }
    public virtual TrustServiceEvaluation FlightTrustNavigation { get; set; }
    public virtual TrustServiceEvaluation PropertyTrustNavigation { get; set; }
    public virtual ICollection<TrustedAgent> TrustedAgents { get; set; }
}