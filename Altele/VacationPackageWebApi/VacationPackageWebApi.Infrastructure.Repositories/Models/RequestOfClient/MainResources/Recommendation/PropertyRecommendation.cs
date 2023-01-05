namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

public class PropertyRecommendation
{
    public PropertyRecommendation()
    {
        Recommendations = new HashSet<Recommendation>();
    }

    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid SourceAgentId { get; set; }
    public Guid InitialAssignedAgentId { get; set; }

    public virtual Agent.Agent InitialAssignedAgent { get; set; }
    public virtual Property.Property Property { get; set; }
    public virtual Agent.Agent SourceAgent { get; set; }
    public virtual ICollection<Recommendation> Recommendations { get; set; }
}