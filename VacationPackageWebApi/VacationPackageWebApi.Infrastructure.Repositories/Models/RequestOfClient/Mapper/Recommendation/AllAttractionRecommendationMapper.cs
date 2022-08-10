using VacationPackageWebApi.Domain.PreferencesPackageResponse.AttractionsPreferencesResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class AllAttractionRecommendationMapper
{
    public static AllAttractionRecommendation ToEntity(Guid sourceAgentId, Guid initialAssignedAgentId)
    {
        return new AllAttractionRecommendation
        {
            Id = Guid.NewGuid(),
            SourceAgentId = sourceAgentId,
            InitialAssignedAgentId = initialAssignedAgentId
        };
    }
}