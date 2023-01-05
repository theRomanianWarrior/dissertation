using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.BusinessLogic;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Application.Services;

public class PreferencesPackageService : IPreferencesPackageService
{
    private readonly IAgentService _agentService;
    private readonly IEvaluationService _evaluationService;
    private readonly IPreferencesPackageRequestRepository _preferencesPackageRepository;
    private readonly IRecommendationService _recommendationService;

    public PreferencesPackageService(IAgentService agentService,
        IPreferencesPackageRequestRepository preferencesPackageRepository, IRecommendationService recommendationService,
        IEvaluationService evaluationService)
    {
        _evaluationService = evaluationService;
        _preferencesPackageRepository = preferencesPackageRepository;
        _recommendationService = recommendationService;
        _agentService = agentService;
    }

    public async Task<PreferencesResponse?> RequestVacationPackage(PreferencesRequest preferencesPayload,
        Guid clientRequestId, DateTime requestTimestamp)
    {
        var preferencesPackageId = await _preferencesPackageRepository.SavePreferences(preferencesPayload);
        await _preferencesPackageRepository.CreateClientRequest(preferencesPayload.CustomerId, preferencesPackageId,
            clientRequestId, requestTimestamp);
        await _agentService.InitializeCustomerPersonalAgentsRate(preferencesPayload.CustomerId);
        CommonRecommendationLogic.StoreAgentsTrustRate(_agentService.GetAllAgentsTrustInOtherAgent());
        return await _recommendationService.GetFullRecommendationsPackage(preferencesPayload);
    }

    public Action SaveRecommendationResponse(PreferencesResponse preferencesResponse, Guid clientRequestId)
    {
        _preferencesPackageRepository.WritePreferencesResponse(preferencesResponse);
        return _preferencesPackageRepository.SaveRecommendation(preferencesResponse, clientRequestId);
    }

    public async Task<Task> SaveEvaluation(ServiceEvaluationDto evaluationOfServices)
    {
        _evaluationService.CalculateEvaluationRatings(ref evaluationOfServices);
        _preferencesPackageRepository.SaveEvaluation(evaluationOfServices);
        UserReportHelper.WriteUserEvaluation(evaluationOfServices);
        await _preferencesPackageRepository.UpdateAgentsSelfExpertRate();
        await _preferencesPackageRepository.UpdateAgentTrustServiceEvaluation(evaluationOfServices);
        await _preferencesPackageRepository.UpdateCustomerPersonalAgentRate(evaluationOfServices);

        return Task.CompletedTask;
    }
}