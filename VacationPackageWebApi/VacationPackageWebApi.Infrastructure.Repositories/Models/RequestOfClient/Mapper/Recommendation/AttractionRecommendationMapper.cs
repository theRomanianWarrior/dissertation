using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Recommendation;

public static class AttractionRecommendationMapper
{
    public static AttractionRecommendation ToEntity(this AttractionBusinessModel attraction,
        Guid allAttractionRecommendationId)
    {
        return new AttractionRecommendation
        {
            Id = Guid.NewGuid(),
            AttractionId = attraction.Xid,
            AllAttractionRecommendationId = allAttractionRecommendationId
        };
    }
}