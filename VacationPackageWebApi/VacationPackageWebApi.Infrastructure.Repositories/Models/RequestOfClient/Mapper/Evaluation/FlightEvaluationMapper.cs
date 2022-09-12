using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class FlightEvaluationMapper
{
    public static FlightEvaluation ToEntity(this FlightEvaluationDto flightEvaluation)
    {
        return new FlightEvaluation
        {
            Id = Guid.NewGuid(),
            Class = flightEvaluation.Class,
            Price = flightEvaluation.Price,
            Company = flightEvaluation.Company,
            FlightDate = flightEvaluation.FlightDate,
            FlightTime = flightEvaluation.FlightTime,
            FlightRating = (float) flightEvaluation.FlightRating!
        };
    }
}