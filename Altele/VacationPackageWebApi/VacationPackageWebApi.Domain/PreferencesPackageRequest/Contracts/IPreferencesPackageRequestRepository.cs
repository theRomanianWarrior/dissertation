using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts;

public interface IPreferencesPackageRequestRepository
{
    public Task<Guid> SavePreferences(PreferencesRequest preferencesPayload);
    public Action SaveRecommendation(PreferencesResponse preferencesResponse, Guid clientRequestId);

    public Task<Task> CreateClientRequest(Guid customerId, Guid preferencesPackageId, Guid clientRequestId,
        DateTime requestTimestamp);

    public void SaveEvaluation(ServiceEvaluationDto evaluationOfServices);
    public Task<Task> UpdateAgentsSelfExpertRate();
    public Task<Task> UpdateAgentTrustServiceEvaluation(ServiceEvaluationDto serviceEvaluation);
    public Task UpdateCustomerPersonalAgentRate(ServiceEvaluationDto evaluationOfServices);
    public Task WritePreferencesResponse(PreferencesResponse preferencesResponse);
}