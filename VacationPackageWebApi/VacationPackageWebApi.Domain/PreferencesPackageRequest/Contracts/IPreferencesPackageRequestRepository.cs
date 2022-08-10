using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts
{
    public interface IPreferencesPackageRequestRepository
    {
        public Task<Guid> SavePreferences(PreferencesRequest preferencesPayload);
        public Action SaveRecommendation(PreferencesResponse preferencesResponse, Guid clientRequestId);

        public Task<Task> CreateClientRequest(Guid customerId, Guid preferencesPackageId, Guid clientRequestId,
            DateTime requestTimestamp);

        public void AddRecommendationToExistingClientRequest(Guid clientRequestId, Guid recommendationId);
    }
}
