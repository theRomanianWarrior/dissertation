using System;
using System.Collections.Generic;
using System.Linq;

namespace TestWebAPI;

public static class StringUtils
{
    public static List<TimeOnly> ConvertStringTimeListToTimeOnly(this string timeData)
    {
        var myStr = timeData.Split(' ');
        return myStr.Select(TimeOnly.Parse).ToList();
    }

    private static DayOfWeek? ConvertStringToDayOfWeek(this string dayOfWeek)
    {
        return dayOfWeek switch
        {
            "Monday" => DayOfWeek.Monday,
            "Tuesday" => DayOfWeek.Tuesday,
            "Wednesday" => DayOfWeek.Wednesday,
            "Thursday" => DayOfWeek.Thursday,
            "Friday" => DayOfWeek.Friday,
            "Saturday" => DayOfWeek.Saturday,
            "Sunday" => DayOfWeek.Sunday,
            _ => null
        };
    }

    public static List<DayOfWeek> ConvertStringDaysOfWeekListToEnumList(this string daysOfFlightList)
    {
        var myStr = daysOfFlightList.Split(' ');

        return myStr.Select(str => (DayOfWeek) ConvertStringToDayOfWeek(str)!).ToList();
    }
}