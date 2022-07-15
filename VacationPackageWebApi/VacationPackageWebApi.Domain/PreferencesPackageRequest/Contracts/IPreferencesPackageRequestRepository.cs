namespace VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts
{
    public interface IPreferencesPackageRequestRepository
    {
        Task SavePreferences(PreferencesRequest preferencesPayload);
        Task<PreferencesResponse> GetFullPackageRecommendations(PreferencesRequest preferencesPayload);
    }
}
