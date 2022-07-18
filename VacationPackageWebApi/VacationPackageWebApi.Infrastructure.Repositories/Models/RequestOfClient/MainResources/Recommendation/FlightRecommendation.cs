namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation
{
    public class FlightRecommendation
    {
        public FlightRecommendation()
        {
            FlightConnections = new HashSet<FlightConnection>();
            Recommendations = new HashSet<Recommendation>();
        }

        public Guid Id { get; set; }
        public Guid SourceAgentId { get; set; }
        public string Status { get; set; }
        public DateOnly FlightDate { get; set; }
        public short Stops { get; set; }

        public virtual Agent.Agent SourceAgent { get; set; }
        public virtual ICollection<FlightConnection> FlightConnections { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
    }
}
