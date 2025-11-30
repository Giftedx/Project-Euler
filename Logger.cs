using System.Diagnostics;

namespace Project_Euler;

/// <summary>
/// Defines the severity levels for log messages.
/// </summary>
public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

/// <summary>
/// A thread-safe static logger class for logging messages to the console and a file.
/// Supports different log levels and scoped logging blocks.
/// </summary>
public static class Logger
{
    private static readonly object _lock = new object();
    private static readonly string LogFile = "euler_solver.log";
    private static LogLevel _minLevel = LogLevel.Info;

    /// <summary>
    /// Sets the minimum log level. Messages below this level will be ignored.
    /// </summary>
    /// <param name="level">The minimum log level to capture.</param>
    public static void SetLogLevel(LogLevel level)
    {
        _minLevel = level;
    }

    /// <summary>
    /// Logs a message at the Debug level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Debug(string message)
    {
        Log(LogLevel.Debug, message);
    }

    /// <summary>
    /// Logs a message at the Info level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Info(string message)
    {
        Log(LogLevel.Info, message);
    }

    /// <summary>
    /// Logs a message at the Warning level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Warning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    /// <summary>
    /// Logs a message at the Error level, optionally including an exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exception">The exception associated with the error (optional).</param>
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

    /// <summary>
    /// Creates a disposable scope for logging.
    /// Logs a start message on creation and a completion message with elapsed time on disposal.
    /// Useful for tracing the execution time of blocks of code.
    /// </summary>
    /// <param name="scopeName">The name of the scope to log.</param>
    /// <returns>An IDisposable object that ends the scope when disposed.</returns>
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
