
namespace CaterpillarControlSystem.App.Services;

public class LoggingService
{

	public static readonly string ProjectRoot =
		Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

	public static string LogsFolder => Path.Combine(ProjectRoot, "logs");

	public readonly string _errorLogPath = Path.Combine(LogsFolder, "error.txt");
	public readonly string _infoLogPath = Path.Combine(LogsFolder, "logs.txt");

	public LoggingService()
	{
		InitializeLogFile(_errorLogPath);
		InitializeLogFile(_infoLogPath);
	}

	public void InitializeLogFile(string filePath)
	{
		if (!File.Exists(filePath))
		{
			Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

			File.Create(filePath).Close();
		}
	}

	public void ClearLogs()
	{
		ClearLogFile(_errorLogPath);
		ClearLogFile(_infoLogPath);
	}

	public static void ClearLogFile(string filePath)
	{
		File.WriteAllText(filePath, string.Empty);
	}

	public void Log(string errorMessage, string type="INFO")
	{
		string filePath = type == "INFO" ? _infoLogPath : _errorLogPath;
		LogToFile(filePath, errorMessage, type);
	}

	public static void LogToFile(string filePath, string message, string type="INFO")
	{
		using var writer = new StreamWriter(filePath, true);
		writer.Write($"\n{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {type} - {message}");
	}
}