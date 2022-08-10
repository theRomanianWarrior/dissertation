using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference
{
    public static class FlightCompaniesPreferenceMapper
    {
        public static FlightCompaniesPreference ToEntity(Guid flightCompanyId, Guid flightPreferenceId)
        {
            return new FlightCompaniesPreference
            {
                Id = Guid.NewGuid(),
                CompanyId = flightCompanyId,
                FlightPreferenceId = flightPreferenceId
            };
        }
    }
}
