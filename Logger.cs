using System.Diagnostics;

namespace Project_Euler;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

public static class Logger
{
    private static readonly object _lock = new object();
    private static readonly string LogFile = "euler_solver.log";
    private static LogLevel _minLevel = LogLevel.Info;

    public static void SetLogLevel(LogLevel level)
    {
        _minLevel = level;
    }

    public static void Debug(string message)
    {
        Log(LogLevel.Debug, message);
    }

    public static void Info(string message)
    {
        Log(LogLevel.Info, message);
    }

    public static void Warning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    public static void Error(string message, Exception? exception = null)
    {
        var fullMessage = exception != null 
            ? $"{message} Exception: {exception.Message}" 
            : message;
        Log(LogLevel.Error, fullMessage);
    }

    private static void Log(LogLevel level, string message)
    {
        if (level < _minLevel) return;

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var logEntry = $"[{timestamp}] [{level}] {message}";

        lock (_lock)
        {
            Console.WriteLine(logEntry);
            
            try
            {
                File.AppendAllText(LogFile, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{timestamp}] [ERROR] Failed to write to log file: {ex.Message}");
            }
        }
    }

    public static IDisposable CreateScope(string scopeName)
    {
        return new LogScope(scopeName);
    }

    private class LogScope : IDisposable
    {
        private readonly string _scopeName;
        private readonly Stopwatch _stopwatch;

        public LogScope(string scopeName)
        {
            _scopeName = scopeName;
            _stopwatch = Stopwatch.StartNew();
            Logger.Info($"Starting: {_scopeName}");
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            Logger.Info($"Completed: {_scopeName} (took {_stopwatch.ElapsedMilliseconds}ms)");
        }
    }
}