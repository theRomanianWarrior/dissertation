namespace VacationPackageWebApi.Domain.Helpers;

public static class DateTimeUtils
{
    public static string GetDayOfWeekFromDate(this DateTime date)
    {
        return date.DayOfWeek.ToString();
    }
}