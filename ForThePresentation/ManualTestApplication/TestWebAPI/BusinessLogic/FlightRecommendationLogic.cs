using System;
using System.Collections.Generic;
using System.Linq;
using TestWebAPI.Enums;
using TestWebAPI.Flight;
using TestWebAPI.PreferencesPackageRequest;
using TestWebAPI.PreferencesPackageResponse.FlightPreferencesResponse;

namespace TestWebAPI.BusinessLogic;

public static class FlightRecommendationLogic
{
    private const int Match = 1;
    private const int NoMatch = 0;
    private const int TotalFlightElementsToMatch = 3;

    public static double CalculateFlightSimilarityRate(PreferencesRequest preferencesRequest,
        FlightRecommendationResponse flight, string flightDirection)
    {
        switch (flightDirection)
        {
            case "Departure":
            {
                var groupFlightTimeByDayPeriods = GroupFlightTimeByDayPeriods(flight.FlightDirectionRecommendation!.DepartureFlightRecommendation!.DepartureTime.ToString("HH:mm"));
                var flightDayMatch =
                    CheckFlightDayMatch(preferencesRequest.DepartureDate.GetDayOfWeekFromDate(), flight.FlightDirectionRecommendation.DepartureFlightRecommendation);
                var flightCompaniesMatchDeparture = CheckFlightCompaniesMatch(
                    preferencesRequest.CustomerFlightNavigation!.DepartureNavigation!.FlightCompaniesNavigationList,
                    flight.FlightDirectionRecommendation.DepartureFlightRecommendation);
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
                var groupFlightTimeByDayPeriods = GroupFlightTimeByDayPeriods(flight.FlightDirectionRecommendation!.ReturnFlightRecommendation!.DepartureTime.ToString("HH:mm"));

                var returnDate = GetFlightReturnDate(preferencesRequest);
                var flightDayMatch = CheckFlightDayMatch(returnDate.GetDayOfWeekFromDate(), flight.FlightDirectionRecommendation!.ReturnFlightRecommendation);

                var flightCompaniesMatchReturn =
                    CheckFlightCompaniesMatch(
                        preferencesRequest.CustomerFlightNavigation!.ReturnNavigation!.FlightCompaniesNavigationList,
                        flight.FlightDirectionRecommendation!.ReturnFlightRecommendation);
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

    private static bool CheckFlightDayMatch(string dayOfFlightPreferred, FlightRecommendationBModel flight)
    {
        return flight.FlightDate.DayOfWeek.ToString().Contains(dayOfFlightPreferred);
    }

    private static bool CheckFlightCompaniesMatch(List<FlightCompaniesPreferenceDto>? preferredFlightCompanies,
        FlightRecommendationBModel flight)
    {
        if (preferredFlightCompanies == null)
            return false;

        return preferredFlightCompanies.Any(flightCompany =>
            flight.FlightConnection.First().Flight.Company.Name == flightCompany.Company.Name);
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

    private static TimeOnly GetEarliestFlightTimeBasedOnPeriodPreference(FlightRecommendationBModel flight,
        DeparturePeriodsPreferenceDto? departurePeriodsPreference)
    {
        var groupedFlightsByDayPeriod = GroupFlightTimeByDayPeriods(flight.DepartureTime.ToString("HH:mm"));
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

    private static DateTime GetFlightReturnDate(PreferencesRequest preferencesRequest)
    {
        return preferencesRequest.DepartureDate.AddDays(preferencesRequest.HolidaysPeriod);
    }
}