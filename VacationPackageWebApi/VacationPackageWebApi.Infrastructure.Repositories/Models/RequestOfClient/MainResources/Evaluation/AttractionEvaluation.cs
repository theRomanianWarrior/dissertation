using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
     public class AttractionEvaluation
    {
        public Guid Id { get; set; }
        public Guid AttractionPointId { get; set; }
        public string EvaluatedAttractionId { get; set; }
        public bool Rate { get; set; }

        public virtual AllAttractionEvaluationPoint AttractionPoint { get; set; }
        public virtual OpenTripMapAttraction EvaluatedAttraction { get; set; }
    }
}
