namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public class TrustedAgent
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public Guid TrustedAgentId { get; set; }
        public Guid TrustedAgentRate { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Agent TrustedAgentNavigation { get; set; }
        public virtual TrustedAgentRate TrustedAgentRateNavigation { get; set; }
    }
}
