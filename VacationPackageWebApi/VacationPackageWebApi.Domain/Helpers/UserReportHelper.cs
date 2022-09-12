using VacationPackageWebApi.Domain.CustomerServicesEvaluation;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Helpers.Models;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;

namespace VacationPackageWebApi.Domain.Helpers;

public static class UserReportHelper
{
    private const string _pathToLogFile = @"C:\Users\emihailov\OneDrive - ENDAVA\Desktop\VacationPackageWebApi\Log\";
    private static string _fileName;

    private static string _currentProcessingAgentSelfExpertRate = string.Empty;
    private static string _currentProcessingAgentSelfExpertService = string.Empty;
    private static bool _personalAgentServiceScoreHeaderInitialized;

    public static void WriteUserPreferencesRequest(PreferencesRequest preferencesPayload, DateTime requestTimestamp)
    {
        _fileName = $"log{requestTimestamp.Date:yy-MM-dd}-{requestTimestamp.ToString("HH-mm-ss")}.txt";
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);

        fileStreamWriter.WriteLine("________________________________________________");
        fileStreamWriter.WriteLine("Preferences of the user");
        fileStreamWriter.WriteLine($"Customer Id = {preferencesPayload.CustomerId}");
        fileStreamWriter.WriteLine($"Departure Date = {preferencesPayload.DepartureDate:dd-MM-yyyy}");
        fileStreamWriter.WriteLine($"Holidays Period = {preferencesPayload.HolidaysPeriod}");
        fileStreamWriter.WriteLine($"Request Timestamp = {requestTimestamp}");
        fileStreamWriter.WriteLine($"Departure City = {preferencesPayload.DepartureCityNavigation.Name}");
        fileStreamWriter.WriteLine($"Destination City = {preferencesPayload.DestinationCityNavigation.Name}");

        fileStreamWriter.WriteLine("Persons by age:");
        fileStreamWriter.WriteLine($"\tAdults: {preferencesPayload.PersonsByAgeNavigation.Adult}");
        fileStreamWriter.WriteLine($"\tChildren: {preferencesPayload.PersonsByAgeNavigation.Children}");
        fileStreamWriter.WriteLine($"\tInfant: {preferencesPayload.PersonsByAgeNavigation.Infant}");

        fileStreamWriter.WriteLine("\nFlight Preferences");
        if (preferencesPayload.CustomerFlightNavigation != null)
        {
            fileStreamWriter.WriteLine("\tDeparture Flight Preferences");
            if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation != null)
            {
                fileStreamWriter.WriteLine("\tFlight Companies:");
                if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.FlightCompaniesNavigationList !=
                    null)
                    foreach (var departureFlightCompanyPreference in preferencesPayload.CustomerFlightNavigation
                                 .DepartureNavigation.FlightCompaniesNavigationList)
                        fileStreamWriter.WriteLine($"\t \t{departureFlightCompanyPreference.Company.Name}");
                fileStreamWriter.WriteLine("\t Departure Day Periods:");
                if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.DeparturePeriodPreference != null)
                {
                    if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.DeparturePeriodPreference
                        .EarlyMorning)
                        fileStreamWriter.WriteLine("\t \t Early Morning");

                    if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.DeparturePeriodPreference
                        .Morning)
                        fileStreamWriter.WriteLine("\t \t Morning");

                    if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.DeparturePeriodPreference
                        .Afternoon)
                        fileStreamWriter.WriteLine("\t \tAfternoon");

                    if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.DeparturePeriodPreference
                        .Night)
                        fileStreamWriter.WriteLine("\t \tNight");
                }

                fileStreamWriter.WriteLine(
                    $"\tClass: {((ClassTypeId) preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.Class!.Class).ToString()}");

                fileStreamWriter.WriteLine("\t \t Stops:");
                if (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.StopsNavigation != null)
                    switch (preferencesPayload.CustomerFlightNavigation!.DepartureNavigation!.StopsNavigation.Type)
                    {
                        case (short) StopsTypePreferenceId.Direct:
                            fileStreamWriter.WriteLine("\t \t Direct");
                            break;
                        case (short) StopsTypePreferenceId.OneStop:
                            fileStreamWriter.WriteLine("\t \t One Stop");
                            break;
                        case (short) StopsTypePreferenceId.TwoOrMoreStops:
                            fileStreamWriter.WriteLine("\t \tTwo or More Stops");
                            break;
                    }
            }

            fileStreamWriter.WriteLine("\n \tReturn Flight Preferences");
            if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation != null)
            {
                fileStreamWriter.WriteLine("\t \tFlight Companies:");
                if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.FlightCompaniesNavigationList !=
                    null)
                    foreach (var returnFlightCompanyPreference in preferencesPayload.CustomerFlightNavigation
                                 .ReturnNavigation
                                 .FlightCompaniesNavigationList)
                        fileStreamWriter.WriteLine($"\t \t{returnFlightCompanyPreference.Company.Name}");
                fileStreamWriter.WriteLine("\tDeparture Day Periods:");
                if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.DeparturePeriodPreference != null)
                {
                    if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.DeparturePeriodPreference
                        .EarlyMorning)
                        fileStreamWriter.WriteLine("\t \tEarly Morning");

                    if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.DeparturePeriodPreference
                        .Morning)
                        fileStreamWriter.WriteLine("\t \tMorning");

                    if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.DeparturePeriodPreference
                        .Afternoon)
                        fileStreamWriter.WriteLine("\t \tAfternoon");

                    if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.DeparturePeriodPreference.Night)
                        fileStreamWriter.WriteLine("\t \tNight");
                }

                fileStreamWriter.WriteLine(
                    $"\tClass: {((ClassTypeId) preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.Class!.Class).ToString()}");

                fileStreamWriter.WriteLine("\tStops:");
                if (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.StopsNavigation != null)
                    switch (preferencesPayload.CustomerFlightNavigation!.ReturnNavigation!.StopsNavigation.Type)
                    {
                        case (short) StopsTypePreferenceId.Direct:
                            fileStreamWriter.WriteLine("\t \tDirect");
                            break;
                        case (short) StopsTypePreferenceId.OneStop:
                            fileStreamWriter.WriteLine("\t \tOne Stop");
                            break;
                        case (short) StopsTypePreferenceId.TwoOrMoreStops:
                            fileStreamWriter.WriteLine("\t \tTwo or More Stops");
                            break;
                    }
            }
        }

        fileStreamWriter.WriteLine("\nProperty Preferences");
        if (preferencesPayload.CustomerPropertyNavigation is {Pets: true})
            fileStreamWriter.WriteLine("\tPets: yes");

        fileStreamWriter.WriteLine("\tProperty Type:");
        if (preferencesPayload.CustomerPropertyNavigation!.PropertyTypeNavigation != null)
        {
            if (preferencesPayload.CustomerPropertyNavigation!.PropertyTypeNavigation.House)
                fileStreamWriter.WriteLine("\t \t House");

            if (preferencesPayload.CustomerPropertyNavigation!.PropertyTypeNavigation.Apartment)
                fileStreamWriter.WriteLine("\t \t Apartment");

            if (preferencesPayload.CustomerPropertyNavigation!.PropertyTypeNavigation.GuestHouse)
                fileStreamWriter.WriteLine("\t \t GuestHouse");

            if (preferencesPayload.CustomerPropertyNavigation!.PropertyTypeNavigation.Hotel)
                fileStreamWriter.WriteLine("\t \t Hotel");
        }

        fileStreamWriter.WriteLine("\tPlace Type:");
        if (preferencesPayload.CustomerPropertyNavigation!.PlaceTypeNavigation != null)
        {
            if (preferencesPayload.CustomerPropertyNavigation!.PlaceTypeNavigation.EntirePlace)
                fileStreamWriter.WriteLine("\t \t EntirePlace");

            if (preferencesPayload.CustomerPropertyNavigation!.PlaceTypeNavigation.PrivateRoom)
                fileStreamWriter.WriteLine("\t \t PrivateRoom");

            if (preferencesPayload.CustomerPropertyNavigation!.PlaceTypeNavigation.SharedRoom)
                fileStreamWriter.WriteLine("\t \t SharedRoom");
        }

        fileStreamWriter.WriteLine("\tRoom and Beds:");
        if (preferencesPayload.CustomerPropertyNavigation!.RoomsAndBedsNavigation != null)
        {
            fileStreamWriter.WriteLine(
                $"\t \tBedrooms: {preferencesPayload.CustomerPropertyNavigation!.RoomsAndBedsNavigation.Bedrooms}");
            fileStreamWriter.WriteLine(
                $"\t \tBeds: {preferencesPayload.CustomerPropertyNavigation!.RoomsAndBedsNavigation.Beds}");
            fileStreamWriter.WriteLine(
                $"\t \tBathrooms: {preferencesPayload.CustomerPropertyNavigation!.RoomsAndBedsNavigation.Bathrooms}");
        }

        fileStreamWriter.WriteLine("\tAmenities:");
        if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation != null)
        {
            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.WiFi)
                fileStreamWriter.WriteLine("\t \t Wifi");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Kitchen)
                fileStreamWriter.WriteLine("\t \t Kitchen");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Washer)
                fileStreamWriter.WriteLine("\t \t Washer");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Dryer)
                fileStreamWriter.WriteLine("\t \t Dryer");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.AirConditioning)
                fileStreamWriter.WriteLine("\t \t Air Conditioning");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Heating)
                fileStreamWriter.WriteLine("\t \t Heating");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Tv)
                fileStreamWriter.WriteLine("\t \t TV");

            if (preferencesPayload.CustomerPropertyNavigation!.AmenitiesNavigation.Iron)
                fileStreamWriter.WriteLine("\t \t Iron");
        }

        fileStreamWriter.WriteLine("\n \tAttractions Preferences:");
        if (preferencesPayload.CustomerAttractionNavigation != null)
        {
            if (preferencesPayload.CustomerAttractionNavigation.Architecture)
                fileStreamWriter.WriteLine("\t \t Architecture");

            if (preferencesPayload.CustomerAttractionNavigation.Cultural)
                fileStreamWriter.WriteLine("\t \t Cultural");

            if (preferencesPayload.CustomerAttractionNavigation.Historical)
                fileStreamWriter.WriteLine("\t \t Historical");

            if (preferencesPayload.CustomerAttractionNavigation.Natural)
                fileStreamWriter.WriteLine("\t \t Natural");

            if (preferencesPayload.CustomerAttractionNavigation.Other)
                fileStreamWriter.WriteLine("\t \t Other");

            if (preferencesPayload.CustomerAttractionNavigation.Religion)
                fileStreamWriter.WriteLine("\t \t Religion");

            if (preferencesPayload.CustomerAttractionNavigation.IndustrialFacilities)
                fileStreamWriter.WriteLine("\t \t Industrial Facilities");
        }

        fileStreamWriter.WriteLine("________________________________________________");
    }

    public static void WritePreferencesResponse(PreferencesResponse preferencesResponse,
        string departureFlightSourceAgentName, string returnFlightSourceAgentName, string propertySourceAgentName,
        string attractionsSourceAgentName)
    {
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);

        fileStreamWriter.WriteLine("________________________________________________");
        fileStreamWriter.WriteLine("Flight recommendations ");
        fileStreamWriter.WriteLine("\n \tDeparture flight: ");
        fileStreamWriter.WriteLine(
            $"\tFlight Date: {preferencesResponse.FlightRecommendationResponse!.FlightDirectionRecommendation!.DepartureFlightRecommendation!.FlightDate:dd-MM-yyyy}");
        fileStreamWriter.WriteLine(
            $"\tDeparture Time: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.DepartureTime:HH:mm}");

        fileStreamWriter.WriteLine("\tClass:");
        switch (preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation
                    .DepartureFlightRecommendation.FlightClass)
        {
            case (short) ClassTypeId.Business:
                fileStreamWriter.WriteLine("\t \tBusiness");
                break;
            case (short) ClassTypeId.Economy:
                fileStreamWriter.WriteLine("\t \tEconomy");
                break;
            case (short) ClassTypeId.First:
                fileStreamWriter.WriteLine("\t \tFirst");
                break;
        }

        fileStreamWriter.WriteLine("\tMore flight information");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture Country: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Country.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture City: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture Airport: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tFlight Company: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.Company.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tFlight is each: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.WeekDaysOfFlight.DaysList}");
        fileStreamWriter.WriteLine(
            $"\t \tAvailable departure time: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.FlightConnection.First().Flight.AvailableDepartureTime.DepartureHour}");

        fileStreamWriter.WriteLine(
            $"\tInitial assigned agent name: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.DepartureFlightRecommendation.InitialAssignedAgentName}");
        fileStreamWriter.WriteLine($"\tSource agent name: {departureFlightSourceAgentName}");


        fileStreamWriter.WriteLine("\n \tReturn flight: ");
        fileStreamWriter.WriteLine(
            $"\tFlight Date: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation!.FlightDate:dd-MM-yyyy}");
        fileStreamWriter.WriteLine(
            $"\tDeparture Time: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.DepartureTime:HH:mm}");

        fileStreamWriter.WriteLine("\tClass ");
        switch (preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation
                    .ReturnFlightRecommendation.FlightClass)
        {
            case (short) ClassTypeId.Business:
                fileStreamWriter.WriteLine("\t \tBusiness");
                break;
            case (short) ClassTypeId.Economy:
                fileStreamWriter.WriteLine("\t \tEconomy");
                break;
            case (short) ClassTypeId.First:
                fileStreamWriter.WriteLine("\t \tFirst");
                break;
        }

        fileStreamWriter.WriteLine("\n \tMore flight information");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture Country: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Country.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture City: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.City.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tDeparture Airport: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.DepartureAirport.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tFlight Company: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.Company.Name}");
        fileStreamWriter.WriteLine(
            $"\t \tFlight is each: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.WeekDaysOfFlight.DaysList}");
        fileStreamWriter.WriteLine(
            $"\t \tAvailable departure time: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.FlightConnection.First().Flight.AvailableDepartureTime.DepartureHour}");

        fileStreamWriter.WriteLine(
            $"\n \tInitial assigned agent name: {preferencesResponse.FlightRecommendationResponse.FlightDirectionRecommendation.ReturnFlightRecommendation.InitialAssignedAgentName}");
        fileStreamWriter.WriteLine($"\tSource agent name: {returnFlightSourceAgentName}");

        fileStreamWriter.WriteLine("\nProperty recommendations");
        fileStreamWriter.WriteLine(
            $"\t City {preferencesResponse.PropertyPreferencesResponse!.PropertyRecommendationBModel.Property.City.Name}");
        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.Pet)
            fileStreamWriter.WriteLine("\t Pet: yes");
        fileStreamWriter.WriteLine("\tAmenities:");
        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage.WiFi)
            fileStreamWriter.WriteLine("\t \t Wifi");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage
            .Kitchen)
            fileStreamWriter.WriteLine("\t \t Kitchen");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage
            .Washer)
            fileStreamWriter.WriteLine("\t \t Washer");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage
            .Dryer)
            fileStreamWriter.WriteLine("\t \t Dryer");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage
            .AirConditioning)
            fileStreamWriter.WriteLine("\t \t Air Conditioning");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage
            .Heating)
            fileStreamWriter.WriteLine("\t \t Heating");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage.Tv)
            fileStreamWriter.WriteLine("\t \t TV");

        if (preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.AmenitiesPackage.Iron)
            fileStreamWriter.WriteLine("\t \t Iron");

        fileStreamWriter.WriteLine(
            $"\tProperty type: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.PropertyType.Type}");
        fileStreamWriter.WriteLine(
            $"\tPlace type: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.PlaceType.Type}");
        fileStreamWriter.WriteLine(
            $"\tBedrooms: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed.Bedroom}");
        fileStreamWriter.WriteLine(
            $"\tBeds: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed.Bed}");
        fileStreamWriter.WriteLine(
            $"\tBathrooms: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.Property.RoomAndBed.Bathroom}");

        fileStreamWriter.WriteLine(
            $"\n \tInitial assigned agent name: {preferencesResponse.PropertyPreferencesResponse.PropertyRecommendationBModel.InitialAssignedAgentName}");
        fileStreamWriter.WriteLine($"\tSource agent name: {propertySourceAgentName}");

        fileStreamWriter.WriteLine("Attractions recommendations ");
        foreach (var attraction in preferencesResponse.AttractionsRecommendationResponse!.AttractionRecommendationList)
        {
            fileStreamWriter.WriteLine($"\t City: {attraction.Attraction.Town}");
            fileStreamWriter.WriteLine($"\t Name: {attraction.Attraction.Name}");
            fileStreamWriter.WriteLine($"\t Kind pattern: {attraction.Attraction.Kinds}");
            fileStreamWriter.WriteLine();
        }

        fileStreamWriter.WriteLine(
            $"\n \tInitial assigned agent name: {preferencesResponse.AttractionsRecommendationResponse.InitialAssignedAgentName}");
        fileStreamWriter.WriteLine($"\tSource agent name: {attractionsSourceAgentName}");

        fileStreamWriter.WriteLine("________________________________________________");
    }

    public static void WriteUserEvaluation(ServiceEvaluationDto serviceEvaluation)
    {
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);
        fileStreamWriter.WriteLine("________________________________________________");
        fileStreamWriter.WriteLine("User's services evaluations");
        fileStreamWriter.WriteLine("\nFlight evaluation");
        fileStreamWriter.WriteLine("Departure flight evaluation");
        fileStreamWriter.WriteLine(
            $"\t Class: {(serviceEvaluation.FlightEvaluation.DepartureNavigation.Class ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Price: {(serviceEvaluation.FlightEvaluation.DepartureNavigation.Price ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Proposed Flight Date: {(serviceEvaluation.FlightEvaluation.DepartureNavigation.FlightDate ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Flight Time: {(serviceEvaluation.FlightEvaluation.DepartureNavigation.FlightTime ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Flight Company: {(serviceEvaluation.FlightEvaluation.DepartureNavigation.Company ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\tFlight Rating: {Math.Round((double) serviceEvaluation.FlightEvaluation.DepartureNavigation.FlightRating!, 2)}");

        fileStreamWriter.WriteLine("\n Return flight evaluation");
        fileStreamWriter.WriteLine(
            $"\t Class: {(serviceEvaluation.FlightEvaluation.ReturnNavigation.Class ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Price: {(serviceEvaluation.FlightEvaluation.ReturnNavigation.Price ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Proposed Flight Date: {(serviceEvaluation.FlightEvaluation.ReturnNavigation.FlightDate ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Flight Time: {(serviceEvaluation.FlightEvaluation.ReturnNavigation.FlightTime ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Flight Company: {(serviceEvaluation.FlightEvaluation.ReturnNavigation.Company ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Flight Rating: {Math.Round((double) serviceEvaluation.FlightEvaluation.ReturnNavigation.FlightRating!, 2)}");

        fileStreamWriter.WriteLine(
            $"\n \t Entire Flight Rating: {Math.Round((double) serviceEvaluation.FlightEvaluation.FinalFlightRating!, 2)}");

        fileStreamWriter.WriteLine("\nProperty evaluation");
        fileStreamWriter.WriteLine(
            $"\t Property type: {(serviceEvaluation.PropertyEvaluation.PropertyType ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Place type: {(serviceEvaluation.PropertyEvaluation.PlaceType ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Rooms and Beds: {(serviceEvaluation.PropertyEvaluation.RoomsAndBeds ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Amenities: {(serviceEvaluation.PropertyEvaluation.Amenities ? "yes" : "no")}");
        fileStreamWriter.WriteLine(
            $"\t Rating: {Math.Round((double) serviceEvaluation.PropertyEvaluation.FinalPropertyRating!, 2)}");


        fileStreamWriter.WriteLine("\nAttractions evaluation");
        foreach (var attractionEvaluation in serviceEvaluation.AttractionEvaluation.AttractionEvaluations)
        {
            fileStreamWriter.WriteLine($"\t Evaluated attraction: {attractionEvaluation.AttractionName}");
            fileStreamWriter.WriteLine($"\t Liked: {(attractionEvaluation.Rate ? "yes" : "no")}");
        }

        fileStreamWriter.WriteLine(
            $"\n \t Final rating: {Math.Round((double) serviceEvaluation.AttractionEvaluation.FinalAttractionEvaluation!, 2)}");

        fileStreamWriter.WriteLine("________________________________________________");
    }

    public static void WriteCustomerPersonalAgentRate(List<PersonalAgentRateLogModel> personalAgentRateLogList,
        bool isFirstCall = true)
    {
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);
        fileStreamWriter.WriteLine("________________________________________________");

        fileStreamWriter.WriteLine("Personal agent rates");

        fileStreamWriter.WriteLine("\t \t \t \t FlightRating \t PropertyRating \t AttractionsRating");

        foreach (var customerPersonalAgentRate in personalAgentRateLogList)
            fileStreamWriter.WriteLine(
                $"\t {customerPersonalAgentRate.AgentName} \t \t {Math.Round(customerPersonalAgentRate.FlightExpertRate, 2)} \t \t {Math.Round(customerPersonalAgentRate.PropertyExpertRate, 2)} \t \t \t {Math.Round(customerPersonalAgentRate.AttractionsExpertRate, 2)}");

        fileStreamWriter.WriteLine("________________________________________________");
    }

    public static void WriteCustomerPersonalAgentServiceRecommendationsCounter(
        PersonalAgentServiceScoreLogModel personalAgentServiceScoreLogModel)
    {
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);
        if (_personalAgentServiceScoreHeaderInitialized == false)
        {
            _personalAgentServiceScoreHeaderInitialized = true;
            fileStreamWriter.WriteLine("________________________________________________");
            fileStreamWriter.WriteLine("\nPersonal agents services score");
            fileStreamWriter.WriteLine("\t \t \t \t FlightScore \t PropertyScore \t AttractionsScore");
        }

        fileStreamWriter.WriteLine(
            $"\t {personalAgentServiceScoreLogModel.AgentName} \t \t {personalAgentServiceScoreLogModel.FlightRecommendationsDoneForCurrentUser} \t \t {personalAgentServiceScoreLogModel.PropertyRecommendationsDoneForCurrentUser} \t \t \t {personalAgentServiceScoreLogModel.AttractionsRecommendationsDoneForCurrentUser}");
    }

    public static void WriteAgentsUpdatedSelfExpertRate(
        PersonalAgentSelfExpertRateLogModel personalAgentSelfExpertRateLogModel)
    {
        using var fileStreamWriter = File.AppendText(_pathToLogFile + _fileName);
        if (_currentProcessingAgentSelfExpertRate == string.Empty)
            fileStreamWriter.WriteLine("\nAgents updating self expert logic");

        if (_currentProcessingAgentSelfExpertRate != personalAgentSelfExpertRateLogModel.AgentName)
        {
            _currentProcessingAgentSelfExpertRate = personalAgentSelfExpertRateLogModel.AgentName;
            _currentProcessingAgentSelfExpertService = personalAgentSelfExpertRateLogModel.ServiceType;

            fileStreamWriter.WriteLine($"\nName: {personalAgentSelfExpertRateLogModel.AgentName}");
            fileStreamWriter.WriteLine($"Service type: {personalAgentSelfExpertRateLogModel.ServiceType}");
        }

        if (_currentProcessingAgentSelfExpertService != personalAgentSelfExpertRateLogModel.ServiceType)
        {
            _currentProcessingAgentSelfExpertService = personalAgentSelfExpertRateLogModel.ServiceType;
            fileStreamWriter.WriteLine($"\nService type: {personalAgentSelfExpertRateLogModel.ServiceType}");
        }

        fileStreamWriter.WriteLine($"\n\tDate of request: {personalAgentSelfExpertRateLogModel.DateOfRequest}");
        fileStreamWriter.WriteLine(
            $"\tDifference between today and request date: {personalAgentSelfExpertRateLogModel.DaysDifferenceFromToday}");
        fileStreamWriter.WriteLine(
            $"\tOriginal value: {personalAgentSelfExpertRateLogModel.ExpertServiceRatings.OriginalValue}");
        fileStreamWriter.WriteLine(
            $"\tActual value: {personalAgentSelfExpertRateLogModel.ExpertServiceRatings.ActualValue}");
    }

    public static void ClearCurrentProcessingAgentSelfExpertLogData()
    {
        _currentProcessingAgentSelfExpertRate = string.Empty;
        _currentProcessingAgentSelfExpertService = string.Empty;
        _personalAgentServiceScoreHeaderInitialized = false;
    }
}