using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class ServiceEvaluationMapper
{
    public static ServiceEvaluation ToEntity(this ServiceEvaluationDto serviceEvaluation, Guid flightEvaluationId,
        Guid propertyEvaluationId, Guid attractionEvaluationId)
    {
        return new ServiceEvaluation
        {
            Id = Guid.NewGuid(),
            FlightEvaluationId = flightEvaluationId,
            PropertyEvaluationId = propertyEvaluationId,
            AttractionEvaluationId = attractionEvaluationId
        };
    }
}