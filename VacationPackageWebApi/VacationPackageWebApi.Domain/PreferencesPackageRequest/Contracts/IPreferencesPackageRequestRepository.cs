namespace VacationPackageWebApi.Domain.PreferencesPackageRequest.Contracts
{
    public interface IPreferencesPackageRequestRepository
    {
        public Task<Task> SavePreferences(PreferencesRequest preferencesPayload);
    }
}
