using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Application.Services
{
    public class PreferencesPackageService : IPreferencesPackageService
    {
        private readonly IPreferencesPackageRequestRepository _preferencesPackageRepository;
        private readonly IRecommendationService _recommendationService;
        private readonly IEvaluationService _evaluationService;
        public PreferencesPackageService(IPreferencesPackageRequestRepository preferencesPackageRepository, IRecommendationService recommendationService, IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
            _preferencesPackageRepository = preferencesPackageRepository;
            _recommendationService = recommendationService;
        }
        
        public async Task<PreferencesResponse?> RequestVacationPackage(PreferencesRequest preferencesPayload, Guid clientRequestId, DateTime requestTimestamp)
        {
            var preferencesPackageId = await _preferencesPackageRepository.SavePreferences(preferencesPayload);
            await _preferencesPackageRepository.CreateClientRequest(preferencesPayload.CustomerId, preferencesPackageId,
                clientRequestId, requestTimestamp);
            return await _recommendationService.GetFullRecommendationsPackage(preferencesPayload);
        }
        
        public Action SaveRecommendationResponse(PreferencesResponse preferencesResponse, Guid clientRequestId)
        {
            return _preferencesPackageRepository.SaveRecommendation(preferencesResponse, clientRequestId);
        }

        public async Task<Task> SaveEvaluation(ServiceEvaluationDto evaluationOfServices)
        {
            _evaluationService.CalculateEvaluationRatings(ref evaluationOfServices);
           await _preferencesPackageRepository.SaveEvaluation(evaluationOfServices);

           return Task.CompletedTask;
        }
    }
}
