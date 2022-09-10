using VacationPackageWepApp.UiDataStoring.Evaluation;

namespace VacationPackageWepApp.UiDataStoring.Singleton;

public sealed class EvaluationServicesSingleton
{
    private static Lazy<ServiceEvaluationDto> _lazy =
        new Lazy<ServiceEvaluationDto>(() => new ServiceEvaluationDto());

    public static ServiceEvaluationDto Instance => _lazy.Value;

    private EvaluationServicesSingleton()
    {
    }

    public static void ResetInstance()
    {
        _lazy =
            new Lazy<ServiceEvaluationDto>(() => new ServiceEvaluationDto());
    }
}