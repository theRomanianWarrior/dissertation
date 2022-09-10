using HttpClients;
using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.UiDataStoring.Evaluation;
using VacationPackageWepApp.UiDataStoring.Preference;
using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;
using VacationPackageWepApp.UiDataStoring.Singleton;

namespace VacationPackageWepApp.Controllers;

[Route("[controller]")]
public class EvaluationController : Controller
{

    [HttpPost("[action]/{departureFlightEvaluation}")]
    public void StoreDepartureFlightEvaluation(string departureFlightEvaluation)
    {
        var listOfLikedDepartureFlightEvaluations = departureFlightEvaluation.Split(", ").ToList();
        var flightEvaluation = new FlightEvaluationDto
        {
            Class = listOfLikedDepartureFlightEvaluations[0] == "yes",
            Company = listOfLikedDepartureFlightEvaluations[1] == "yes",
            FlightDate = listOfLikedDepartureFlightEvaluations[2] == "yes",
            FlightTime = listOfLikedDepartureFlightEvaluations[3] == "yes"
        };

        EvaluationServicesSingleton.Instance.FlightEvaluation = new FlightDirectionEvaluationDto
        {
            DepartureNavigation = flightEvaluation
        };
    }
    
    [HttpPost("[action]/{returnFlightEvaluation}")]
    public void StoreReturnFlightEvaluation(string returnFlightEvaluation)
    {
        var listOfLikedReturnFlightEvaluations = returnFlightEvaluation.Split(", ").ToList();
        EvaluationServicesSingleton.Instance.FlightEvaluation.ReturnNavigation = new FlightEvaluationDto
        {
            Class = listOfLikedReturnFlightEvaluations[0] == "yes", ///////////////verifica consecventa corecta
            Company = listOfLikedReturnFlightEvaluations[1] == "yes",
            FlightDate = listOfLikedReturnFlightEvaluations[2] == "yes",
            FlightTime = listOfLikedReturnFlightEvaluations[3] == "yes"
        };
    }
    
    [HttpPost("[action]/{propertyEvaluation}")]
    public void StorePropertyEvaluation(string propertyEvaluation)
    {
        var listOfPropertyEvaluations = propertyEvaluation.Split(", ").ToList();
        EvaluationServicesSingleton.Instance.PropertyEvaluation = new PropertyEvaluationDto()
        {
            PropertyType = listOfPropertyEvaluations[0] == "yes",
            PlaceType = listOfPropertyEvaluations[1] == "yes",
            RoomsAndBeds = listOfPropertyEvaluations[2] == "yes",
            Amenities = listOfPropertyEvaluations[3] == "yes"
        };
    }
    
    [HttpPost("[action]/{attractionEvaluation}")]
    public async Task StoreAttractionsEvaluation(string attractionEvaluation)
    {
        var listOfAttractionsEvaluations = attractionEvaluation.Split(", ").ToList();

        for (var attractionIterator = 0; attractionIterator < listOfAttractionsEvaluations.Count; attractionIterator++)
        {
            EvaluationServicesSingleton.Instance.AttractionEvaluation.AttractionEvaluations[attractionIterator].Rate =
                listOfAttractionsEvaluations[attractionIterator] == "yes";
        }

        await SaveToDatabaseUserEvaluation();
    }

    public async Task SaveToDatabaseUserEvaluation()
    {
        using var evaluationClient =
            new GenericRestfulCrudHttpClient<ServiceEvaluationDto, ActionResult>("http://localhost:7071/", "VacationPackage/SaveEvaluations/");
        await evaluationClient.PostAsync<PreferencesResponse>(EvaluationServicesSingleton.Instance);
    }
}