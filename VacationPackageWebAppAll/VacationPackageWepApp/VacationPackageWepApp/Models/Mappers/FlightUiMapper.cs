using VacationPackageWepApp.UiDataStoring.Enums;
using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;

namespace VacationPackageWepApp.Models.Mappers;

public static class FlightUiMapper
{
    public static FlightUiModel ToFlightUiModel(this PreferencesResponse preferencesResponse)
    {
        
        return new FlightUiModel()
        {
            DepartureFlightUiModel = new DepartureFlightUiModel()
            {
                FlightDate = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightDate.Date.ToString("dd-MM-yyyy"),
                DepartureTime = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.DepartureTime.ToString("HH:mm"),
                Stops = "Direct",
                Class = ((ClassTypeId)preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightClass).ToString(),
                Company = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.Company.Name,
                Airport = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.Name,
                City = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Name,
                Country = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Country.Name
            },
            ReturnFlightUiModel = new ReturnFlightUiModel()
            {
                FlightDate = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightDate.Date.ToString("dd-MM-yyyy"),
                DepartureTime = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.DepartureTime.ToString("HH:mm"),
                Stops = "Direct",
                Class = ((ClassTypeId)preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightClass).ToString(),
                Company = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.Company.Name,
                Airport = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.Name,
                City = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Name,
                Country = preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Country.Name
            }
        };
    }
}