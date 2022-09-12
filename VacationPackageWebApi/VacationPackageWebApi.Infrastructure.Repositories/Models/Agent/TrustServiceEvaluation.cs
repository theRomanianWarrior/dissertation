namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent;

public class TrustServiceEvaluation
{
    public TrustServiceEvaluation()
    {
        TrustedAgentRateAttractionsTrustNavigations = new HashSet<TrustedAgentRate>();
        TrustedAgentRateFlightTrustNavigations = new HashSet<TrustedAgentRate>();
        TrustedAgentRatePropertyTrustNavigations = new HashSet<TrustedAgentRate>();
    }

    public Guid Id { get; set; }
    public short PositiveEvaluation { get; set; }
    public short NegativeEvaluation { get; set; }
    public short NeutralEvaluation { get; set; }
    public DateTime LastPositiveEvaluation { get; set; }
    public DateTime LastNegativeEvaluation { get; set; }

    public virtual ICollection<TrustedAgentRate> TrustedAgentRateAttractionsTrustNavigations { get; set; }
    public virtual ICollection<TrustedAgentRate> TrustedAgentRateFlightTrustNavigations { get; set; }
    public virtual ICollection<TrustedAgentRate> TrustedAgentRatePropertyTrustNavigations { get; set; }
}