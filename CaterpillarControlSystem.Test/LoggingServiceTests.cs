using CaterpillarControlSystem.App.Services;

namespace CaterpillarControlSystem.tests
{
    public class LoggingServiceTests
    {
    [Fact]
    public void LoggingService_Constructor_ShouldInitializeLogFiles()
    {
        var loggingService = new LoggingService();
        Assert.True(File.Exists(loggingService._errorLogPath));
        Assert.True(File.Exists(loggingService._infoLogPath));
    }

    [Fact]
    public void ClearLogs_ShouldEmptyAllLogFiles()
    {
        var loggingService = new LoggingService();
        loggingService.Log("Movement message");
        loggingService.Log("Error message", "ERROR");

        loggingService.ClearLogs();

        Assert.Equal(string.Empty, File.ReadAllText(loggingService._errorLogPath));
        Assert.Equal(string.Empty, File.ReadAllText(loggingService._infoLogPath));
    }

    [Fact]
    public void LogError_ShouldAppendErrorMessageToLogFile()
    {
        var loggingService = new LoggingService();
        const string errorMessage = "Test error";
        loggingService.Log(errorMessage, "ERROR");

        var lastEntry = File.ReadLines(loggingService._errorLogPath).Last();
        Assert.Contains(errorMessage, lastEntry);
    }

    [Fact]
    public void LogMovement_ShouldAppendMovementMessageToLogFile()
    {
        var loggingService = new LoggingService();
        const string movementMessage = "Test movement";
        loggingService.Log(movementMessage);

        var lastEntry = File.ReadLines(loggingService._infoLogPath).Last();
        Assert.Contains(movementMessage, lastEntry);
    }
}
}
