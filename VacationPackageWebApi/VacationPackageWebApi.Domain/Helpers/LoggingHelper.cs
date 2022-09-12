namespace VacationPackageWebApi.Domain.Helpers;

public static class LoggingHelper
{
    static ReaderWriterLock locker = new ReaderWriterLock();
    public static void WriteDebug(this string text)
    {
        try
        {
            locker.AcquireWriterLock(100); //You might wanna change timeout value 
            System.IO.File.AppendAllLines(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), "debug.txt"), new[] { text });
            File.AppendAllLines(@"C:\Users\emihailov\OneDrive - ENDAVA\Desktop\VacationPackageWebApi\Log\AgentsCommunicationLog\communication.txt", new []{text});
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }
}