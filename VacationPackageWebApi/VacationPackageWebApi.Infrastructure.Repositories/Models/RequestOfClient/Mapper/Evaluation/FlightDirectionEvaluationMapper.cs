using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Mapper.Evaluation;

public static class FlightDirectionEvaluationMapper
{
    public static FlightDirectionEvaluation ToEntity(this FlightDirectionEvaluationDto flightDirectionEvaluation,
        Guid departureFlightEvaluationId, Guid returnFlightEvaluationId)
    {
        return new FlightDirectionEvaluation
        {
            Id = Guid.NewGuid(),
            Departure = departureFlightEvaluationId,
            Return = returnFlightEvaluationId,
            TotalFlightRating = (float) flightDirectionEvaluation.FinalFlightRating!
        };
    }
}