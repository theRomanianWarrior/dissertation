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
        public bool Class { get; set; }
        public bool Price { get; set; }
        public bool Company { get; set; }
        public bool FlightDate { get; set; }
        public bool FlightTime { get; set; }
        public float FlightRating { get; set; }

        public virtual ICollection<FlightDirectionEvaluation> FlightDirectionEvaluationDepartureNavigations { get; set; }
        public virtual ICollection<FlightDirectionEvaluation> FlightDirectionEvaluationReturnNavigations { get; set; }
    }
}
