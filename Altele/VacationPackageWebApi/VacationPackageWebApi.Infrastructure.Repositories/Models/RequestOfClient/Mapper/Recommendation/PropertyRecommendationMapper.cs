using VacationPackageWebApi.Domain.PreferencesPackageResponse.PropertyPreferencesResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class PropertyRecommendationMapper
{
    public static PropertyRecommendation ToEntity(this PropertyRecommendationBModel propertyRecommendation,
        Guid initialAssignedAgentId)
    {
        return new PropertyRecommendation
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyRecommendation.Property.Id,
            SourceAgentId = propertyRecommendation.SourceAgentId,
            InitialAssignedAgentId = initialAssignedAgentId
        };
    }
}