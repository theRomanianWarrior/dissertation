using System;

namespace TestWebAPI;

public static class DateTimeUtils
{
    public static string GetDayOfWeekFromDate(this DateTime date)
    
    {
        return date.DayOfWeek.ToString();
    }
}