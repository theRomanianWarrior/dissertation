namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public class FlightDirectionEvaluation
    {
        public FlightDirectionEvaluation()
        {
            ServiceEvaluations = new HashSet<ServiceEvaluation>();
        }

        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }
        public float TotalFlightRating { get; set; }

        public virtual FlightEvaluation DepartureNavigation { get; set; }
        public virtual FlightEvaluation ReturnNavigation { get; set; }
        public virtual ICollection<ServiceEvaluation> ServiceEvaluations { get; set; }
    }
}
