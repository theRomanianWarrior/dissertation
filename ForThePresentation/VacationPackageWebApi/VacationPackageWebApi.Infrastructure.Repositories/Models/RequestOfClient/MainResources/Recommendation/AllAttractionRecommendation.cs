namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

public class AllAttractionRecommendation
{
    public AllAttractionRecommendation()
    {
        AttractionRecommendations = new HashSet<AttractionRecommendation>();
        Recommendations = new HashSet<Recommendation>();
    }

    public Guid Id { get; set; }
    public Guid SourceAgentId { get; set; }
    public Guid InitialAssignedAgentId { get; set; }

    public virtual Agent.Agent InitialAssignedAgent { get; set; }
    public virtual Agent.Agent SourceAgent { get; set; }
    public virtual ICollection<AttractionRecommendation> AttractionRecommendations { get; set; }
    public virtual ICollection<Recommendation> Recommendations { get; set; }
}