using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public class AllAttractionEvaluationPoint
    {
        public AllAttractionEvaluationPoint()
        {
            AttractionEvaluations = new HashSet<AttractionEvaluation>();
            ServiceEvaluations = new HashSet<ServiceEvaluation>();
        }

        public Guid Id { get; set; }
        public short FinalPropertyEvaluation { get; set; }

        public virtual ICollection<AttractionEvaluation> AttractionEvaluations { get; set; }
        public virtual ICollection<ServiceEvaluation> ServiceEvaluations { get; set; }
    }
}
