namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Customer
{
    public class CustomerPersonalAgentRate
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AgentId { get; set; }

        public virtual Agent.Agent Agent { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
