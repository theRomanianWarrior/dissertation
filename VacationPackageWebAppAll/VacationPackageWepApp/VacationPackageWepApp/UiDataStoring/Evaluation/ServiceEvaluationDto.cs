namespace VacationPackageWepApp.UiDataStoring.Evaluation
{
    public class ServiceEvaluationDto
    {
        public Guid ClientRequestId { get; set; }
        public AllAttractionEvaluationPointDto AttractionEvaluation { get; set; }
        public FlightDirectionEvaluationDto FlightEvaluation { get; set; }
        public PropertyEvaluationDto PropertyEvaluation { get; set; }
    }
}
