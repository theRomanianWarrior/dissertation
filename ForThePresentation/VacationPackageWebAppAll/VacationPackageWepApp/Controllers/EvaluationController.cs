using HttpClients;
using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.UiDataStoring.Evaluation;
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
            Class = listOfLikedDepartureFlightEvaluations[0] == "true",
            Company = listOfLikedDepartureFlightEvaluations[1] == "true",
            FlightDate = listOfLikedDepartureFlightEvaluations[2] == "true",
            FlightTime = listOfLikedDepartureFlightEvaluations[3] == "true",
            Price = true
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
            Class = listOfLikedReturnFlightEvaluations[0] == "true",
            Company = listOfLikedReturnFlightEvaluations[1] == "true",
            FlightDate = listOfLikedReturnFlightEvaluations[2] == "true",
            FlightTime = listOfLikedReturnFlightEvaluations[3] == "true",
            Price = true
        };
    }

    [HttpPost("[action]/{propertyEvaluation}")]
    public void StorePropertyEvaluation(string propertyEvaluation)
    {
        var listOfPropertyEvaluations = propertyEvaluation.Split(", ").ToList();
        EvaluationServicesSingleton.Instance.PropertyEvaluation = new PropertyEvaluationDto
        {
            PropertyType = listOfPropertyEvaluations[0] == "true",
            PlaceType = listOfPropertyEvaluations[1] == "true",
            RoomsAndBeds = listOfPropertyEvaluations[2] == "true",
            Amenities = listOfPropertyEvaluations[3] == "true"
        };
    }

    [HttpPost("[action]/{attractionEvaluation}")]
    public async Task<IActionResult> StoreAttractionsEvaluation(string attractionEvaluation)
    {
        var listOfAttractionsEvaluations = attractionEvaluation.Split(", ").ToList();

        for (var attractionIterator = 0; attractionIterator < listOfAttractionsEvaluations.Count; attractionIterator++)
            EvaluationServicesSingleton.Instance.AttractionEvaluation.AttractionEvaluations[attractionIterator].Rate =
                listOfAttractionsEvaluations[attractionIterator] == "true";

        await SaveToDatabaseUserEvaluation();
        await ResetEvaluationInstance();
        return new JsonResult("");
    }

    public async Task SaveToDatabaseUserEvaluation()
    {
        using var evaluationClient =
            new GenericRestfulCrudHttpClient<ServiceEvaluationDto, ActionResult>("http://localhost:7071/",
                "VacationPackage/SaveEvaluations/");
        await evaluationClient.PostAsync<string>(EvaluationServicesSingleton.Instance);
    }

    public Task ResetEvaluationInstance()
    {
        EvaluationServicesSingleton.ResetInstance();

        return Task.CompletedTask;
    }
}