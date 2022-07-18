using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public class FlightEvaluation
    {
        public FlightEvaluation()
        {
            ServiceEvaluations = new HashSet<ServiceEvaluation>();
        }

        public Guid Id { get; set; }
        public short Class { get; set; }
        public short TypeOfFlight { get; set; }
        public short Stops { get; set; }
        public short Connections { get; set; }
        public short FinalFlightRating { get; set; }

        public virtual ICollection<ServiceEvaluation> ServiceEvaluations { get; set; }
    }
}
