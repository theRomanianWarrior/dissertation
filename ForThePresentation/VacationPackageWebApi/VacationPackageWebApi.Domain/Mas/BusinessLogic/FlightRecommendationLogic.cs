using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;
using VacationPackageWebApi.Domain.Enums;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Helpers;
using VacationPackageWebApi.Domain.Mas.Singleton;
using VacationPackageWebApi.Domain.PreferencesPackageRequest;
using VacationPackageWebApi.Domain.PreferencesPackageResponse;
using VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;
using static VacationPackageWebApi.Domain.Mas.BusinessLogic.CommonRecommendationLogic;

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
                var flightDayMatch =
                    CheckFlightDayMatch(preferencesRequest.DepartureDate.GetDayOfWeekFromDate(), flight);
                var flightCompaniesMatchDeparture = CheckFlightCompaniesMatch(
                    preferencesRequest.CustomerFlightNavigation!.DepartureNavigation!.FlightCompaniesNavigationList,
                    flight);
                var preferenceDeparturePeriodMatch = CheckPreferenceDeparturePeriodMatch(
                    preferencesRequest.CustomerFlightNavigation.DepartureNavigation.DeparturePeriodPreference,
                    groupFlightTimeByDayPeriods);
                return ((double) (flightCompaniesMatchDeparture ? Match : NoMatch) +
                        (preferenceDeparturePeriodMatch ? Match : NoMatch) +
                        (flightDayMatch ? Match : NoMatch)) /
                       TotalFlightElementsToMatch;
            }
            case "Return":
            {
                var returnDate = GetFlightReturnDate(preferencesRequest);
                var flightDayMatch = CheckFlightDayMatch(returnDate.GetDayOfWeekFromDate(), flight);

                var flightCompaniesMatchReturn =
                    CheckFlightCompaniesMatch(
                        preferencesRequest.CustomerFlightNavigation!.ReturnNavigation!.FlightCompaniesNavigationList,
                        flight);
                var preferenceReturnPeriodMatch =
                    CheckPreferenceDeparturePeriodMatch(
                        preferencesRequest.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference,
                        groupFlightTimeByDayPeriods);
                return ((double) (flightCompaniesMatchReturn ? Match : NoMatch) +
                        (preferenceReturnPeriodMatch ? Match : NoMatch) +
                        (flightDayMatch ? Match : NoMatch)) /
                       TotalFlightElementsToMatch;
            }
        }

        return 0.0d;
    }

    public static void FulfillFlightDefaultPreferencesWithCheapestOffer(ref PreferencesRequest preferencesRequest)
    {
        preferencesRequest.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        preferencesRequest.CustomerFlightNavigation.DepartureNavigation ??= new FlightPreferenceDto();
        preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class ??= new FlightClassDto
        {
            Class = (short) ClassTypeId.Economy
        };
        preferencesRequest.CustomerFlightNavigation.DepartureNavigation.StopsNavigation ??=
            new StopsTypePreferenceDto
            {
                Type = (short) StopsTypePreferenceId.Direct
            };


        preferencesRequest.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
        preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class ??= new FlightClassDto
        {
            Class = (short) ClassTypeId.Economy
        };
        preferencesRequest.CustomerFlightNavigation.ReturnNavigation.StopsNavigation ??=
            new StopsTypePreferenceDto
            {
                Type = (short) StopsTypePreferenceId.Direct
            };
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

    private static TimeOnly? GetEarliestFlightThatMatchWithPreferences(
        DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        Dictionary<DayPeriods, List<TimeOnly>> groupedFlightsByDayPeriod)
    {
        if (departurePeriodsPreference.IsFulfilledEarlyMorningPreference(groupedFlightsByDayPeriod))
            if (groupedFlightsByDayPeriod.ContainsKey(DayPeriods.EarlyMorning))
                return groupedFlightsByDayPeriod[DayPeriods.EarlyMorning].First();

        if (departurePeriodsPreference.IsFulfilledMorningPreference(groupedFlightsByDayPeriod))
            if (groupedFlightsByDayPeriod.ContainsKey(DayPeriods.Morning))
                return groupedFlightsByDayPeriod[DayPeriods.Morning].First();

        if (departurePeriodsPreference.IsFulfilledAfternoonPreference(groupedFlightsByDayPeriod))
            if (groupedFlightsByDayPeriod.ContainsKey(DayPeriods.Afternoon))
                return groupedFlightsByDayPeriod[DayPeriods.Afternoon].First();

        if (departurePeriodsPreference.IsFulfilledNightPreference(groupedFlightsByDayPeriod))
            if (groupedFlightsByDayPeriod.ContainsKey(DayPeriods.Night))
                return groupedFlightsByDayPeriod[DayPeriods.Night].First();

        return null;
    }

    private static TimeOnly GetEarliestFlightTimeBasedOnPeriodPreference(FlightBusinessModel flight,
        DeparturePeriodsPreferenceDto? departurePeriodsPreference)
    {
        var groupedFlightsByDayPeriod = GroupFlightTimeByDayPeriods(flight.AvailableDepartureTime.DepartureHour);
        // check each true preference about part of the day with grouped flights by parts of the day
        // if false, get closest one
        TimeOnly? firstAvailableFlightTime = null;

        if (departurePeriodsPreference == null)
            foreach (var (_, value) in groupedFlightsByDayPeriod)
                if (value.Any())
                    return value.First();

        firstAvailableFlightTime =
            GetEarliestFlightThatMatchWithPreferences(departurePeriodsPreference, groupedFlightsByDayPeriod);

        if (firstAvailableFlightTime != null)
            return (TimeOnly) firstAvailableFlightTime;

        // if no matches, get closest flight for user preferred period. First find the earliest preferred and search
        // for latest flights. If not found, search early than earliest preferred.

        var earliestDayPeriodPreference = GetEarliestDayPeriodPreference(departurePeriodsPreference!);

        if (earliestDayPeriodPreference != null)
        {
            for (var dayPeriod = (DayPeriods) earliestDayPeriodPreference; dayPeriod <= DayPeriods.Night; dayPeriod++)
            {
                if (dayPeriod == DayPeriods.Night) break;

                var nexDayPeriod = dayPeriod + 1;
                if (groupedFlightsByDayPeriod[nexDayPeriod].Any())
                    return groupedFlightsByDayPeriod[nexDayPeriod].First();
            }

            for (var dayPeriod = (DayPeriods) earliestDayPeriodPreference; dayPeriod >= DayPeriods.Morning; dayPeriod--)
            {
                if (dayPeriod == DayPeriods.Morning) return TimeOnly.MinValue;

                var previousDayPeriod = dayPeriod - 1;
                if (groupedFlightsByDayPeriod[previousDayPeriod].Any())
                    return groupedFlightsByDayPeriod[previousDayPeriod].First();
            }
        }

        return TimeOnly.MinValue;
    }

    private static DayPeriods? GetEarliestDayPeriodPreference(DeparturePeriodsPreferenceDto departurePeriodsPreference)
    {
        return departurePeriodsPreference.EarlyMorning ? DayPeriods.EarlyMorning :
            departurePeriodsPreference.Morning ? DayPeriods.Morning :
            departurePeriodsPreference.Afternoon ? DayPeriods.Afternoon :
            departurePeriodsPreference.Night ? DayPeriods.Night : null;
    }

    private static Dictionary<DayPeriods, List<TimeOnly>> GroupFlightTimeByDayPeriods(string flightDepartureTimeList)
    {
        var departureHours = flightDepartureTimeList.ConvertStringTimeListToTimeOnly();
        var flightGroup = new Dictionary<DayPeriods, List<TimeOnly>>();

        for (var dayPeriod = DayPeriods.EarlyMorning; dayPeriod <= DayPeriods.Night; dayPeriod++)
            flightGroup.Add(dayPeriod, new List<TimeOnly>());

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

            if (hour.IsNightTime()) flightGroup[DayPeriods.Night].Add(hour);
        }

        var sortedHours = new Dictionary<DayPeriods, List<TimeOnly>>();
        foreach (var (key, value) in flightGroup)
        {
            var orderedTime = value.OrderBy(x => x).ToList();
            sortedHours.Add(key, orderedTime);
        }

        return sortedHours;
    }

    private static void StoreInMemoryDepartureFlightRecommendation(object recommendationPopulationLock,
        FlightRecommendationBModel flightRecommendation)
    {
        lock (recommendationPopulationLock)
        {
            InitializeFlightRecommendationResponseIfNull();

            (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!
                .FlightRecommendationResponse!
                .FlightDirectionRecommendation!.DepartureFlightRecommendation ??= flightRecommendation;
        }
    }

    private static void StoreInMemoryReturnFlightRecommendation(object recommendationPopulationLock,
        FlightRecommendationBModel flightRecommendation)
    {
        lock (recommendationPopulationLock)
        {
            InitializeFlightRecommendationResponseIfNull();

            (MasEnvironmentSingleton.Instance.Memory["PreferencesResponse"] as PreferencesResponse)!
                .FlightRecommendationResponse!
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
        var departureCityFlights = FindFlightsByDepartureByDestination(tourismAgent,
            preferencesRequest.DepartureCityNavigation.Name, preferencesRequest.DestinationCityNavigation.Name);

        if (departureCityFlights.Any())
        {
            if (preferencesRequest.CustomerFlightNavigation?.DepartureNavigation == null)
            {
                var randomOptimalDepartureFlight =
                    departureCityFlights.ElementAt(Random.Next(departureCityFlights.Count));

                var departureWeekDaysOfRandomFlight = randomOptimalDepartureFlight.WeekDaysOfFlight.DaysList
                    .ConvertStringDaysOfWeekListToEnumList().ToList();
                var earliestDepartureDateOfRandomFlight =
                    (DateTime) GetTheEarliestDepartureDateOfFlight(departureWeekDaysOfRandomFlight,
                        preferencesRequest.DepartureDate)!;
                preferencesRequest.DepartureDate = earliestDepartureDateOfRandomFlight;

                var mostOptimalFlightTime = randomOptimalDepartureFlight.AvailableDepartureTime.DepartureHour
                    .ConvertStringTimeListToTimeOnly().First();
                var randomDepartureFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId,
                    initialAssignedAgentName,
                    randomOptimalDepartureFlight, (short) ClassTypeId.Economy,
                    earliestDepartureDateOfRandomFlight, mostOptimalFlightTime);

                StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock,
                    randomDepartureFlightRecommendation);
                return true;
            }

            var optimalDepartureFlight =
                FindOptimalFlightByDirectionByUserPreference(preferencesRequest, departureCityFlights,
                    "Departure");

            preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class ??= new FlightClassDto
            {
                Class = (short) ClassTypeId.Economy
            };

            var departureWeekFlight = optimalDepartureFlight.WeekDaysOfFlight.DaysList
                .ConvertStringDaysOfWeekListToEnumList().ToList();
            var earliestDepartureDateOfFlight =
                (DateTime) GetTheEarliestDepartureDateOfFlight(departureWeekFlight, preferencesRequest.DepartureDate)!;
            preferencesRequest.DepartureDate = earliestDepartureDateOfFlight;

            var earliestFlightTime = GetEarliestFlightTimeBasedOnPeriodPreference(optimalDepartureFlight,
                preferencesRequest.CustomerFlightNavigation.DepartureNavigation.DeparturePeriodPreference);

            var departureFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId,
                initialAssignedAgentName,
                optimalDepartureFlight, preferencesRequest.CustomerFlightNavigation.DepartureNavigation.Class.Class,
                earliestDepartureDateOfFlight, earliestFlightTime);

            StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock, departureFlightRecommendation);
            return true;
        }

        return false;
    }

    public static bool FindOptimalReturnFlightAndStoreInMemory(Guid sourceAgentId,
        string initialAssignedAgentName, object recommendationPopulationLock, TourismAgent tourismAgent,
        PreferencesRequest preferencesRequest)
    {
        var returnCityFlights = FindFlightsByDepartureByDestination(tourismAgent,
            preferencesRequest.DestinationCityNavigation.Name, preferencesRequest.DepartureCityNavigation.Name);

        if (returnCityFlights.Any())
        {
            if (preferencesRequest.CustomerFlightNavigation?.ReturnNavigation == null)
            {
                var randomOptimalReturnFlight = returnCityFlights.ElementAt(Random.Next(returnCityFlights.Count));

                var preferredReturnFlightDate =
                    preferencesRequest.DepartureDate.AddDays(preferencesRequest.HolidaysPeriod);

                var returnWeekDaysOfRandomFlight = randomOptimalReturnFlight.WeekDaysOfFlight.DaysList
                    .ConvertStringDaysOfWeekListToEnumList().ToList();
                var earliestReturnDateOfRandomFlight = (DateTime) GetTheMostOptimalReturnDateOfFlight(
                    returnWeekDaysOfRandomFlight, preferencesRequest.DepartureDate, preferredReturnFlightDate,
                    preferencesRequest.HolidaysPeriod)!;

                var mostOptimalFlightTime = randomOptimalReturnFlight.AvailableDepartureTime.DepartureHour
                    .ConvertStringTimeListToTimeOnly().First();
                var randomReturnFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId,
                    initialAssignedAgentName,
                    randomOptimalReturnFlight, (short) ClassTypeId.Economy,
                    earliestReturnDateOfRandomFlight, mostOptimalFlightTime);

                StoreInMemoryDepartureFlightRecommendation(recommendationPopulationLock,
                    randomReturnFlightRecommendation);
                return true;
            }

            var optimalArrivalFlight =
                FindOptimalFlightByDirectionByUserPreference(preferencesRequest, returnCityFlights,
                    "Return");

            preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class ??= new FlightClassDto
            {
                Class = (short) ClassTypeId.Economy
            };

            var earliestFlightTime = GetEarliestFlightTimeBasedOnPeriodPreference(optimalArrivalFlight,
                preferencesRequest.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference);
            var flightReturnDate = GetFlightReturnDate(preferencesRequest);

            var returnWeekDaysOfFlight = optimalArrivalFlight.WeekDaysOfFlight.DaysList
                .ConvertStringDaysOfWeekListToEnumList().ToList();
            var earliestReturnDateOfFlight = (DateTime) GetTheMostOptimalReturnDateOfFlight(returnWeekDaysOfFlight,
                preferencesRequest.DepartureDate, flightReturnDate, preferencesRequest.HolidaysPeriod)!;

            var returnFlightRecommendation = CreateFlightRecommendationBModel(sourceAgentId, initialAssignedAgentName,
                optimalArrivalFlight, preferencesRequest.CustomerFlightNavigation.ReturnNavigation.Class.Class,
                earliestReturnDateOfFlight, earliestFlightTime);

            StoreInMemoryReturnFlightRecommendation(recommendationPopulationLock, returnFlightRecommendation);
            return true;
        }

        return false;
    }

    private static DateTime? GetTheMostOptimalReturnDateOfFlight(List<DayOfWeek> availableReturnDaysOfWeekFlight,
        DateTime actualDepartureFlightDate, DateTime preferredReturnFlightDate, int vacationPeriod)
    {
        if (DateTime.Compare(actualDepartureFlightDate, preferredReturnFlightDate) >= 0)
            preferredReturnFlightDate = actualDepartureFlightDate.AddDays(vacationPeriod);

        var returnPreferredFlightDayOfWeek = preferredReturnFlightDate.DayOfWeek;

        if (!availableReturnDaysOfWeekFlight.Contains(returnPreferredFlightDayOfWeek))
        {
            if (returnPreferredFlightDayOfWeek != DayOfWeek.Sunday)
            {
                for (var dayOfWeekDescInc = returnPreferredFlightDayOfWeek;
                     dayOfWeekDescInc >= DayOfWeek.Sunday;
                     --dayOfWeekDescInc)
                    if (availableReturnDaysOfWeekFlight.Contains(dayOfWeekDescInc))
                    {
                        var daysEarlierPreferredReturnFlight = returnPreferredFlightDayOfWeek - dayOfWeekDescInc;
                        var mostEarlyCloseToPreferredReturnFlight =
                            preferredReturnFlightDate.AddDays(-daysEarlierPreferredReturnFlight);

                        if (DateTime.Compare(actualDepartureFlightDate, mostEarlyCloseToPreferredReturnFlight) < 0)
                            return mostEarlyCloseToPreferredReturnFlight;
                    }

                for (var weekEarlierInc = DayOfWeek.Saturday;
                     weekEarlierInc > returnPreferredFlightDayOfWeek;
                     weekEarlierInc--)
                    if (availableReturnDaysOfWeekFlight.Contains(weekEarlierInc))
                    {
                        var daysEarlierPreferredReturnFlight = (short) returnPreferredFlightDayOfWeek -
                            (returnPreferredFlightDayOfWeek - DayOfWeek.Sunday) + (DayOfWeek.Saturday - weekEarlierInc);
                        var mostEarlyCloseToPreferredReturnFlight =
                            preferredReturnFlightDate.AddDays(-daysEarlierPreferredReturnFlight);

                        if (DateTime.Compare(actualDepartureFlightDate, mostEarlyCloseToPreferredReturnFlight) < 0)
                            return mostEarlyCloseToPreferredReturnFlight;
                    }

                //Above is the search in early days than preferred one.
                //Now it's time to search in later days. It's the same
                //algorithm as for departure flight day search 
                var earliestButLaterThanPreferredFlightDay =
                    GetTheEarliestDepartureDateOfFlight(availableReturnDaysOfWeekFlight, preferredReturnFlightDate);
                return earliestButLaterThanPreferredFlightDay;
            }
            else
            {
                returnPreferredFlightDayOfWeek = DayOfWeek.Saturday;
                for (var dayOfWeekDescInc = returnPreferredFlightDayOfWeek;
                     dayOfWeekDescInc > DayOfWeek.Sunday;
                     --dayOfWeekDescInc)
                    if (availableReturnDaysOfWeekFlight.Contains(dayOfWeekDescInc))
                    {
                        var daysEarlierPreferredReturnFlight =
                            returnPreferredFlightDayOfWeek - dayOfWeekDescInc +
                            1; // +1 because we begun from Saturday, not Sunday
                        var mostEarlyCloseToPreferredReturnFlight =
                            preferredReturnFlightDate.AddDays(-daysEarlierPreferredReturnFlight);

                        if (DateTime.Compare(actualDepartureFlightDate, mostEarlyCloseToPreferredReturnFlight) < 0)
                            return mostEarlyCloseToPreferredReturnFlight;
                    }

                var earliestButLaterThanPreferredFlightDay =
                    GetTheEarliestDepartureDateOfFlight(availableReturnDaysOfWeekFlight, preferredReturnFlightDate);
                return earliestButLaterThanPreferredFlightDay;
            }
        }

        return preferredReturnFlightDate;
    }


    private static DateTime? GetTheEarliestDepartureDateOfFlight(List<DayOfWeek> daysOfWeekFlight,
        DateTime preferredDepartureFlightDate)
    {
        var preferredFlightDayOfWeek = preferredDepartureFlightDate.DayOfWeek;

        if (!daysOfWeekFlight.Contains(preferredFlightDayOfWeek))
        {
            if (preferredFlightDayOfWeek != DayOfWeek.Saturday)
            {
                for (var dayOfWeekAscInc = preferredFlightDayOfWeek + 1;
                     dayOfWeekAscInc <= DayOfWeek.Saturday;
                     dayOfWeekAscInc++)
                    if (daysOfWeekFlight.Contains(dayOfWeekAscInc))
                    {
                        //Select day of current week and exit
                        var dayToTheEarliestFlight = dayOfWeekAscInc - preferredFlightDayOfWeek;
                        return preferredDepartureFlightDate.AddDays(dayToTheEarliestFlight);
                    }

                for (var nextWeekInc = DayOfWeek.Sunday; nextWeekInc < preferredFlightDayOfWeek; nextWeekInc++)
                    if (daysOfWeekFlight.Contains(nextWeekInc))
                    {
                        var dayToTheEarliestFlight =
                            (short) (DayOfWeek.Saturday - preferredFlightDayOfWeek) + (short) nextWeekInc +
                            1; // +1 because the counter is from 0
                        return preferredDepartureFlightDate.AddDays(dayToTheEarliestFlight);
                    }
            }

            for (var nextWeekInc = DayOfWeek.Sunday; nextWeekInc < preferredFlightDayOfWeek; nextWeekInc++)
                if (daysOfWeekFlight.Contains(nextWeekInc))
                {
                    var dayToTheEarliestFlight =
                        (short) (DayOfWeek.Saturday - preferredFlightDayOfWeek) + (short) nextWeekInc + 1;
                    return preferredDepartureFlightDate.AddDays(dayToTheEarliestFlight);
                }
        }
        else
        {
            return preferredDepartureFlightDate;
        }

        return null;
    }

    private static bool IsFulfilledEarlyMorningPreference(
        this DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        return departurePeriodsPreference!.EarlyMorning && groupedFlightTimes[DayPeriods.EarlyMorning].Any();
    }

    private static bool IsFulfilledMorningPreference(this DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        return departurePeriodsPreference!.Morning && groupedFlightTimes[DayPeriods.Morning].Any();
    }

    private static bool IsFulfilledAfternoonPreference(this DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        return departurePeriodsPreference!.Afternoon && groupedFlightTimes[DayPeriods.Afternoon].Any();
    }

    private static bool IsFulfilledNightPreference(this DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        return departurePeriodsPreference!.Night && groupedFlightTimes[DayPeriods.Night].Any();
    }

    private static bool CheckPreferenceDeparturePeriodMatch(DeparturePeriodsPreferenceDto? departurePeriodsPreference,
        IReadOnlyDictionary<DayPeriods, List<TimeOnly>> groupedFlightTimes)
    {
        if (departurePeriodsPreference == null)
            return false;

        return departurePeriodsPreference.IsFulfilledEarlyMorningPreference(groupedFlightTimes) ||
               departurePeriodsPreference.IsFulfilledMorningPreference(groupedFlightTimes) ||
               departurePeriodsPreference.IsFulfilledAfternoonPreference(groupedFlightTimes) ||
               departurePeriodsPreference.IsFulfilledNightPreference(groupedFlightTimes);
    }

    private static FlightRecommendationBModel CreateFlightRecommendationBModel(Guid sourceAgentId,
        string initialAssignedAgentName, FlightBusinessModel optimalFlight, short flightClass,
        DateTime departureDate, TimeOnly mostOptimalFlightTime)
    {
        var optimalTime = mostOptimalFlightTime.ToTimeSpan();

        return new FlightRecommendationBModel
        {
            SourceAgentId = sourceAgentId,
            InitialAssignedAgentName = initialAssignedAgentName,
            DepartureTime =
                DateTime.MinValue.AddHours(optimalTime.Hours).AddMinutes(optimalTime.Minutes),
            FlightClass = flightClass,
            FlightConnection = new List<FlightConnectionBModel>
            {
                new()
                {
                    Flight = optimalFlight
                }
            },
            FlightDate = departureDate,
            Status = "Up-to-date",
            Stops = 0
        };
    }

    private static HashSet<FlightBusinessModel> FindFlightsByDepartureByDestination(AgentLocalDb agentLocalDb,
        string startFlightCityName, string endFlightCityName)
    {
        return agentLocalDb.FlightsList.Where(f =>
            f.DepartureAirport.City.Name == startFlightCityName &&
            f.ArrivalAirport.City.Name == endFlightCityName).ToHashSet();
    }

    private static DateTime GetFlightReturnDate(PreferencesRequest preferencesRequest)
    {
        return preferencesRequest.DepartureDate.AddDays(preferencesRequest.HolidaysPeriod);
    }
}