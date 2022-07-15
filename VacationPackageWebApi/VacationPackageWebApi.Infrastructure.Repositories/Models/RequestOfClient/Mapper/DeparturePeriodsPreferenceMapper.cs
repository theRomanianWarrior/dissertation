using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper
{
    public static class DeparturePeriodsPreferenceMapper
    {
        public static DeparturePeriodsPreference ToEntity(this DeparturePeriodsPreferenceDto departurePeriodsPreferenceDto)
        {
            return new DeparturePeriodsPreference
            {
                Id = Guid.NewGuid(),
                Afternoon = departurePeriodsPreferenceDto.Afternoon,
                EarlyMorning = departurePeriodsPreferenceDto.EarlyMorning,
                Morning = departurePeriodsPreferenceDto.Morning,
                Night = departurePeriodsPreferenceDto.Night
            };
        }
    }
}
