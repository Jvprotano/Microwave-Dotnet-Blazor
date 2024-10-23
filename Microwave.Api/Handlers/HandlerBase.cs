namespace Microwave.Api.Handlers;

public abstract class HandlerBase
{
    protected void LogToFile(Exception ex)
    {
        var logFilePath = "logs/exceptions.log";

        var logDetails = $@"
        Time: {DateTime.UtcNow}
        Exception: {ex.Message}
        Inner Exception: {ex.InnerException?.Message}
        Stack Trace: {ex.StackTrace}";

        File.AppendAllText(logFilePath, logDetails);
    }
}
