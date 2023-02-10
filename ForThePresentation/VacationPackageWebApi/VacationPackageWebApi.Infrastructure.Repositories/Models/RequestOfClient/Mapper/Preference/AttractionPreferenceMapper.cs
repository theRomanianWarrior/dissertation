using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Preference;

public static class AttractionPreferenceMapper
{
    public static AttractionPreference ToEntity(this AttractionPreferenceDto attractionPreferenceDto)
    {
        return new AttractionPreference
        {
            Id = Guid.NewGuid(),
            Architecture = attractionPreferenceDto.Architecture,
            Cultural = attractionPreferenceDto.Cultural,
            Historical = attractionPreferenceDto.Historical,
            IndustrialFacilities = attractionPreferenceDto.IndustrialFacilities,
            Natural = attractionPreferenceDto.Natural,
            Other = attractionPreferenceDto.Other,
            Religion = attractionPreferenceDto.Religion
        };
    }
}