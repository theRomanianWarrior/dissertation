namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public class FlightEvaluation
    {
        public FlightEvaluation()
        {
            FlightDirectionEvaluationDepartureNavigations = new HashSet<FlightDirectionEvaluation>();
            FlightDirectionEvaluationReturnNavigations = new HashSet<FlightDirectionEvaluation>();
        }

        public Guid Id { get; set; }
        public short Class { get; set; }
        public short Price { get; set; }
        public short Company { get; set; }
        public short FinalFlightRating { get; set; }

        public virtual ICollection<FlightDirectionEvaluation> FlightDirectionEvaluationDepartureNavigations { get; set; }
        public virtual ICollection<FlightDirectionEvaluation> FlightDirectionEvaluationReturnNavigations { get; set; }
    }
}
