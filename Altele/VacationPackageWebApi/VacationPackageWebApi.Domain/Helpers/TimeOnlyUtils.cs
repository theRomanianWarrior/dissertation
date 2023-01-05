namespace VacationPackageWebApi.Domain.Helpers;

public static class TimeOnlyUtils
{
    private const int T1IsLaterT2 = 1;
    private const int T1IsEarlierT2 = -1;

    public static bool IsEarlyMorningTime(this TimeOnly time)
    {
        return TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("00:00")) == T1IsLaterT2 &&
               TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("05:00")) == T1IsEarlierT2;
    }

    public static bool IsMorningTime(this TimeOnly time)
    {
        return TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("05:01")) == T1IsLaterT2 &&
               TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("12:00")) == T1IsEarlierT2;
    }

    public static bool IsAfternoonTime(this TimeOnly time)
    {
        return TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("12:01")) == T1IsLaterT2 &&
               TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("18:00")) == T1IsEarlierT2;
    }

    public static bool IsNightTime(this TimeOnly time)
    {
        return TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("18:01")) == T1IsLaterT2 &&
               TimeSpan.Compare(time.ToTimeSpan(), TimeSpan.Parse("23:59")) == T1IsEarlierT2;
    }
}