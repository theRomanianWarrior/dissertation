namespace VacationPackageWebApi.Domain.Helpers;

public static class LoggingHelper
{
    private static readonly ReaderWriterLock locker = new();

    public static void WriteDebug(this string text)
    {
        try
        {
            locker.AcquireWriterLock(100);
            File.AppendAllLines(
                @"C:\Users\emihailov\OneDrive - ENDAVA\Desktop\VacationPackageWebApi\Log\AgentsCommunicationLog\communication.txt",
                new[] {text});
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }
}