namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
     public class ServiceEvaluation
    {
        public ServiceEvaluation()
        {
            ClientRequests = new HashSet<ClientRequest>();
        }

        public Guid Id { get; set; }
        public Guid FlightEvaluationId { get; set; }
        public Guid PropertyEvaluationId { get; set; }
        public Guid AttractionEvaluationId { get; set; }

        public virtual AllAttractionEvaluationPoint AttractionEvaluation { get; set; }
        public virtual FlightDirectionEvaluation FlightEvaluation { get; set; }
        public virtual PropertyEvaluation PropertyEvaluation { get; set; }
        public virtual ICollection<ClientRequest> ClientRequests { get; set; }
    }
}
