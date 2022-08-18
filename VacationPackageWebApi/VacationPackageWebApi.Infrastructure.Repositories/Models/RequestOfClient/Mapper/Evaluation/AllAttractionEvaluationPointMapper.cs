using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class AllAttractionEvaluationPointMapper
{
    public static AllAttractionEvaluationPoint ToEntity(this AllAttractionEvaluationPointDto allAttractionEvaluationPoint)
    {
        return new AllAttractionEvaluationPoint
        {
            Id = Guid.NewGuid(),
            FinalAttractionEvaluation = (float) allAttractionEvaluationPoint.FinalAttractionEvaluation!
        };
    }
}