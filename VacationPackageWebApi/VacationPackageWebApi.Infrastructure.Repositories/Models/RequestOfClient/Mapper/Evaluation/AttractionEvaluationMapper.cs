using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class AttractionEvaluationMapper
{
    public static AttractionEvaluation ToEntity(this AttractionEvaluationDto attractionEvaluation,
        Guid attractionPointId)
    {
        return new AttractionEvaluation
        {
            Id = Guid.NewGuid(),
            AttractionPointId = attractionPointId,
            EvaluatedAttractionId = attractionEvaluation.AttractionId,
            Rate = attractionEvaluation.Rate
        };
    }
}