using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;

namespace VacationPackageWebApi.Domain.Mas.BusinessLogic;

public static class FlightRecommendationLogic
{
    private const int TotalFlightElementsToMatch = 3;  
    private static readonly Random Random = new();
    
    private static FlightBusinessModel FindOptimalFlightByDirectionByUserPreference(
        PreferencesRequest preferencesRequest,
        HashSet<FlightBusinessModel> flights, string direction)
    {
        var bestSimilarityRate = 0.0d;
        var randomFlightIndex = Random.Next(flights.Count);
        var optimalFlight = flights.ElementAt(randomFlightIndex);

        foreach (var flight in flights)
        {
            var currentSimilarityRate = CalculateFlightSimilarityRate(preferencesRequest, flight, direction);
            if (currentSimilarityRate > bestSimilarityRate)
            {
                optimalFlight = flight;
                bestSimilarityRate = currentSimilarityRate;
            }
        }

        return optimalFlight;
    }

    private static double CalculateFlightSimilarityRate(PreferencesRequest preferencesRequest,
        FlightBusinessModel flight, string flightDirection)
    {
        var groupFlightTimeByDayPeriods = GroupFlightTimeByDayPeriods(flight.AvailableDepartureTime.DepartureHour);

        switch (flightDirection)
        {
            case "Departure":
            {
                var flightDayMatch= CheckFlightDayMatch(preferencesRequest.DepartureDate.GetDayOfWeekFromDate(), flight);
                var flightCompaniesMatchDeparture = CheckFlightCompaniesMatch(
                    preferencesRequest.CustomerFlightNavigation.DepartureNavigation.FlightCompaniesNavigationList,
                    flight);
                var preferenceDeparturePeriodMatch = CheckPreferenceDeparturePeriodMatch(
                    preferencesRequest.CustomerFlightNavigation.DepartureNavigation, groupFlightTimeByDayPeriods);
                return ((double) (flightCompaniesMatchDeparture ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch) + 
                                 (preferenceDeparturePeriodMatch ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch) +
                                 (flightDayMatch ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch)) /
                                    TotalFlightElementsToMatch;
            }
            case "Return":
            {
                var returnDate = GetFlightReturnDate(preferencesRequest);
                var flightDayMatch= CheckFlightDayMatch(returnDate.GetDayOfWeekFromDate(), flight);

                var flightCompaniesMatchReturn =
                    CheckFlightCompaniesMatch(
                        preferencesRequest.CustomerFlightNavigation.ReturnNavigation.FlightCompaniesNavigationList,
                        flight);
                var preferenceReturnPeriodMatch =
                    CheckPreferenceDeparturePeriodMatch(preferencesRequest.CustomerFlightNavigation.ReturnNavigation,
                        groupFlightTimeByDayPeriods);
                return ((double) (flightCompaniesMatchReturn ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch) + 
                                 (preferenceReturnPeriodMatch ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch) +
                                 (flightDayMatch ? CommonRecommendationLogic.Match : CommonRecommendationLogic.NoMatch)) /
                                    TotalFlightElementsToMatch;
            }
        }

        return 0.0d;
    }

    public static void FulfillFlightDefaultPreferencesWithCheapestOffer(ref PreferencesRequest preferencesRequest)
    {
        if (preferencesRequest.CustomerFlightNavigation is {DepartureNavigation: { }})
        {
            preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class ??= new FlightClassDto()
            {
                Class = (short) ClassTypeId.Economy
            };                
        }

        
        // if (preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class.Class == (short) ClassTypeId.Default)
        //     preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class.Class = (short) ClassTypeId.Economy;

        if (preferencesRequest.CustomerFlightNavigation is {DepartureNavigation: { }})
        {
            preferencesRequest.CustomerFlightNavigation.DepartureNavigation.StopsNavigation ??=
                new StopsTypePreferenceDto
                {
                    Type = (short) StopsTypePreferenceId.Direct
                };
        }

        if (preferencesRequest.CustomerFlightNavigation is {ReturnNavigation: { }})
        {
            preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class ??= new FlightClassDto()
            {
                Class = (short) ClassTypeId.Economy
            };                
        }

        //if (preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class.Class == (short) ClassTypeId.Default)
        //    preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class.Class = (short) ClassTypeId.Economy;

        if (preferencesRequest.CustomerFlightNavigation is {ReturnNavigation: { }})
        {
            preferencesRequest.CustomerFlightNavigation.ReturnNavigation.StopsNavigation ??=
                new StopsTypePreferenceDto
                {
                    Type = (short) StopsTypePreferenceId.Direct
                };
        }
    }

    private static bool CheckFlightDayMatch(string dayOfFlightPreferred, FlightBusinessModel flight)
    {
        return flight.WeekDaysOfFlight.DaysList.Contains(dayOfFlightPreferred);
    }
    
    private static bool CheckFlightCompaniesMatch(List<FlightCompaniesPreferenceDto>? preferredFlightCompanies,
        FlightBusinessModel flight)
    {
        if (preferredFlightCompanies == null)
            return false;
        
        return preferredFlightCompanies.Any(flightCompany =>
            flight.Company.Name == flightCompany.Company.Name);
    }

    private static TimeOnly GetEarliestFlightTimeBasedOnPeriodPreference(FlightBusinessModel flight)
    {
        var groupedFlightsByDayPeriod = GroupFlightTimeByDayPeriods(flight.AvailableDepartureTime.DepartureHour);
        foreach (var (_, value) in groupedFlightsByDayPeriod)
        {
            if (value.Any())
                return value.First();
        }

        return TimeOnly.MinValue;
    }

    private static Dictionary<DayPeriods, List<TimeOnly>> GroupFlightTimeByDayPeriods(string flightDepartureTimeList)
    {
        var departureHours = flightDepartureTimeList.ConvertStringTimeListToTimeOnly();
        var flightGroup = new Dictionary<DayPeriods, List<TimeOnly>>();

        for (var dayPeriod = DayPeriods.EarlyMorning; dayPeriod <= DayPeriods.Night; dayPeriod++)
        {
            flightGroup.Add(dayPeriod, new List<TimeOnly>());
        }

        foreach (var hour in departureHours)
        {
            if (hour.IsEarlyMorningTime())
            {
                flightGroup[DayPeriods.EarlyMorning].Add(hour);
                continue;
            }

            if (hour.IsMorningTime())
            {
                flightGroup[DayPeriods.Morning].Add(hour);
                continue;
            }

            if (hour.IsAfternoonTime())
            {
                flightGroup[DayPeriods.Afternoon].Add(hour);
                continue;
            }

            if (hour.IsNightTime())
            {
                flightGroup[DayPeriods.Night].Add(hour);
            }
        }

        return flightGroup;
    }

    private static void StoreInMemoryDepartureFlightRecommendation(object recommendationPopulationLock, FlightRecommendationBModel flightRecommendation)
    {
        lock (recommendationPopulationLock)
        {
            InitializeFlightRecommendationResponseIfNull();
            
            (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!.FlightRecommendationResponse!
                .FlightDirectionRecommendation!.DepartureFlightRecommendation ??= flightRecommendation;
        }
    }

    private static void StoreInMemoryReturnFlightRecommendation(object recommendationPopulationLock, FlightRecommendationBModel flightRecommendation)
    {
        lock (recommendationPopulationLock)
        {
            InitializeFlightRecommendationResponseIfNull();
            
            (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!.FlightRecommendationResponse!
                .FlightDirectionRecommendation!.ReturnFlightRecommendation ??= flightRecommendation;
        }
    }

    private static void InitializeFlightRecommendationResponseIfNull()
    {
        (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!
            .FlightRecommendationResponse ??= new FlightRecommendationResponse();
        
        (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!
            .FlightRecommendationResponse!.FlightDirectionRecommendation ??= new FlightDirectionRecommendationBModel();
    }
    
    public static bool FindOptimalDepartureFlightAndStoreInMemory(Guid sourceAgentId,
        string initialAssignedAgentName, object recommendationPopulationLock, TourismAgent tourismAgent,
        PreferencesRequest preferencesRequest)
    {
        var departureCityFlights = FindFlightsByDepartureByDestination(tourismAgent, preferencesRequest);

        if (departureCityFlights.Any())
        {
            if (preferencesRequest.CustomerFlightNavigation?.DepartureNavigation == null)
            {
                var randomOptimalDepartureFlight = departureCityFlights.ElementAt(Random.Next(departureCityFlights.Count));
                var randomDepartureFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId, initialAssignedAgentName,
                    randomOptimalDepartureFlight, (short) ClassTypeId.Economy,
                    preferencesRequest.DepartureDate);
                
                StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock, randomDepartureFlightRecommendation);
                return true;
            }
            
            var optimalDepartureFlight =
                FindOptimalFlightByDirectionByUserPreference(preferencesRequest, departureCityFlights,
                    "Departure");

            preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class ??= new FlightClassDto()
            {
                Class = (short) ClassTypeId.Economy
            };
            
            var departureFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId, initialAssignedAgentName,
                optimalDepartureFlight, preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class.Class,
                preferencesRequest.DepartureDate);
            
            StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock, departureFlightRecommendation);
            return true;
        }

        return false;
    }

    public static bool FindOptimalReturnFlightAndStoreInMemory(Guid sourceAgentId,
        string initialAssignedAgentName, object recommendationPopulationLock, TourismAgent tourismAgent,
        PreferencesRequest preferencesRequest)
    {
        var returnCityFlights = FindFlightsByDepartureByDestination(tourismAgent, preferencesRequest);

        if (returnCityFlights.Any())
        {
            if (preferencesRequest.CustomerFlightNavigation?.ReturnNavigation == null)
            {
                var randomOptimalReturnFlight = returnCityFlights.ElementAt(Random.Next(returnCityFlights.Count));
                var randomReturnFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId, initialAssignedAgentName,
                    randomOptimalReturnFlight, (short) ClassTypeId.Economy,
                    preferencesRequest.DepartureDate);
                
                StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock, randomReturnFlightRecommendation);
                return true;
            }
            
            var optimalArrivalFlight =
                FindOptimalFlightByDirectionByUserPreference(preferencesRequest, returnCityFlights,
                    "Return");

            preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class ??= new FlightClassDto()
            {
                Class = (short) ClassTypeId.Economy
            };
            
            var returnFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId, initialAssignedAgentName,
                optimalArrivalFlight, preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class.Class,
                GetFlightReturnDate(preferencesRequest));
                
            StoreInMemoryReturnFlightRecommendation(recommendationPopulationLock, returnFlightRecommendation);
            return true;
        }

        return false;
    }
    
    private static bool CheckPreferenceDeparturePeriodMatch(FlightPreferenceDto flightPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        var departurePeriodPreference = flightPreference.DeparturePeriodPreference;

        if (departurePeriodPreference == null)
            return false;
        
        return departurePeriodPreference.EarlyMorning && groupedFlightTimes[DayPeriods.EarlyMorning].Any() ||
               departurePeriodPreference.Morning && groupedFlightTimes[DayPeriods.Morning].Any() ||
               departurePeriodPreference.Afternoon && groupedFlightTimes[DayPeriods.Afternoon].Any() ||
               departurePeriodPreference.Night && groupedFlightTimes[DayPeriods.Night].Any();
    }

    private static FlightRecommendationBModel CreateFlightRecommendationBModel(Guid sourceAgentId,
        string initialAssignedAgentName, FlightBusinessModel optimalFlight, short flightClass,
        DateTime departureDate)
    {
        var earliestFlightTime = GetEarliestFlightTimeBasedOnPeriodPreference(optimalFlight);

        return new FlightRecommendationBModel
        {
            SourceAgentId = sourceAgentId,
            InitialAssignedAgentName = initialAssignedAgentName,
            DepartureTime =
                DateTime.MinValue.AddHours(earliestFlightTime.Hour).AddMinutes(earliestFlightTime.Minute),
            FlightClass = flightClass,
            FlightConnection = new List<FlightConnectionBModel>
            {
                new()
                {
                    Flight = optimalFlight
                }
            },
            FlightDate = departureDate,
            Status = "Up-to-date",////////////////////////////////////
            Stops = 0
        };
    }

    private static HashSet<FlightBusinessModel> FindFlightsByDepartureByDestination(AgentLocalDb agentLocalDb,
        PreferencesRequest preferencesRequest)
    {
        return agentLocalDb.FlightsList.Where(f =>
            f.DepartureAirport.City.Name == preferencesRequest.DepartureCityNavigation.Name && 
            f.ArrivalAirport.City.Name == preferencesRequest.DestinationCityNavigation.Name).ToHashSet();
    }

    private static DateTime GetFlightReturnDate(PreferencesRequest preferencesRequest)
    {
        return preferencesRequest.DepartureDate.AddDays(preferencesRequest.HolidaysPeriod);
    }
}