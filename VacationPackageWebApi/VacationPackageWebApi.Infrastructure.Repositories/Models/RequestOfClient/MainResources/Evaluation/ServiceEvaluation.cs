namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public record ServiceEvaluation
    {
        public Guid Id { get; set; }
        public Guid FlightEvaluationId { get; set; }
        public Guid PropertyEvaluationId { get; set; }
        public Guid AttractionEvaluationId { get; set; }

        public virtual AllAttractionEvaluationPoint AttractionEvaluation { get; set; }
        public virtual FlightEvaluation FlightEvaluation { get; set; }
        public virtual ProperyEvaluation PropertyEvaluation { get; set; }
    }
}
