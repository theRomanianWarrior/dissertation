using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.CustomerServicesEvaluation;

namespace VacationPackageWebApi.Domain.Services;

public class EvaluationService : IEvaluationService
{
    public void CalculateEvaluationRatings(ref ServiceEvaluationDto evaluationOfServices)
    {
        evaluationOfServices.FlightEvaluation.DepartureNavigation.FlightRating =
            CalculateFlightEvaluationRating(evaluationOfServices.FlightEvaluation.DepartureNavigation);
        evaluationOfServices.FlightEvaluation.ReturnNavigation.FlightRating =
            CalculateFlightEvaluationRating(evaluationOfServices.FlightEvaluation.ReturnNavigation);
        evaluationOfServices.FlightEvaluation.FinalFlightRating =
            (evaluationOfServices.FlightEvaluation.DepartureNavigation.FlightRating +
             evaluationOfServices.FlightEvaluation.ReturnNavigation.FlightRating) / 2f;
        
        evaluationOfServices.PropertyEvaluation.FinalPropertyRating = CalculatePropertyEvaluationRating(evaluationOfServices.PropertyEvaluation);

        evaluationOfServices.AttractionEvaluation.FinalAttractionEvaluation = CalculateAttractionEvaluationRating(evaluationOfServices.AttractionEvaluation.AttractionEvaluations);
    }

    private float CalculateFlightEvaluationRating(FlightEvaluationDto flightEvaluation)
    {
        return ((float)Convert.ToInt16(flightEvaluation.Class) + Convert.ToInt16(flightEvaluation.Company) +
                Convert.ToInt16(flightEvaluation.FlightDate) + Convert.ToInt16(flightEvaluation.FlightTime)) / 4f;
    }
    
    private float CalculatePropertyEvaluationRating(PropertyEvaluationDto propertyEvaluation)
    {
        return ((float)Convert.ToInt16(propertyEvaluation.Amenities) + Convert.ToInt16(propertyEvaluation.PlaceType) + Convert.ToInt16(propertyEvaluation.PropertyType) +
                Convert.ToInt16(propertyEvaluation.RoomsAndBeds)) / 4f;
    }
    
    private float CalculateAttractionEvaluationRating(List<AttractionEvaluationDto> attractionEvaluations)
    {
        var finalAttractionEvaluationRating = 0.0f;
        foreach (var attractionEvaluation in attractionEvaluations)
        {
            finalAttractionEvaluationRating += Convert.ToInt16(attractionEvaluation.Rate);
        }

        return finalAttractionEvaluationRating / attractionEvaluations.Count;
    }
    
}