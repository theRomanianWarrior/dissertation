using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class PropertyEvaluationMapper
{
    public static PropertyEvaluation ToEntity(this PropertyEvaluationDto propertyEvaluation)
    {
        return new PropertyEvaluation
        {
            Id = Guid.NewGuid(),
            PropertyType = propertyEvaluation.PropertyType,
            PlaceType = propertyEvaluation.PlaceType,
            RoomsAndBeds = propertyEvaluation.RoomsAndBeds,
            Amenities = propertyEvaluation.Amenities,
            FinalPropertyRating = (float) propertyEvaluation.FinalPropertyRating!
        };
    } 
}