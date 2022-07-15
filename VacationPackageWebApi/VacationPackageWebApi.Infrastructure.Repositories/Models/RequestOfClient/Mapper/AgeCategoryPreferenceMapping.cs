﻿using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper
{
    public static class AgeCategoryPreferenceMapping
    {
        public static AgeCategoryPreference ToEntity(this AgeCategoryPreferenceDto ageCategoryPreferenceDto)
        {
            return new AgeCategoryPreference
            {
                Id = Guid.NewGuid(),
                Adult = ageCategoryPreferenceDto.Adult,
                Children = ageCategoryPreferenceDto.Children,
                Infant = ageCategoryPreferenceDto.Infant
            };
        }
    }
}
