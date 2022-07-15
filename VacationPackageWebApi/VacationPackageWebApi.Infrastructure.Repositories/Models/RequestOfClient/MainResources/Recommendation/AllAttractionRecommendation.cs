namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation
{
    public record AllAttractionRecommendation
    {
        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }

        public virtual Agent.Agent SourceAgent { get; set; }
    }
}
