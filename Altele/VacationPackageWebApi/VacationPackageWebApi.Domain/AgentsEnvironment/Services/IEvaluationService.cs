using VacationPackageWebApi.Domain.CustomerServicesEvaluation;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IEvaluationService
{
    public void CalculateEvaluationRatings(ref ServiceEvaluationDto evaluationOfServices);
}