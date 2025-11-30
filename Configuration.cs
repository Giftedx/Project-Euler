using System.Text.Json;

namespace Project_Euler;

/// <summary>
/// Singleton configuration manager for the Project Euler application.
/// Handles loading and saving settings for benchmarking, logging, and problem execution.
/// </summary>
public class Configuration
{
    private static Configuration? _instance;
    private static readonly object _lock = new object();
    private const string ConfigFile = "euler_config.json";

    public BenchmarkSettings Benchmark { get; set; } = new();
    public LoggingSettings Logging { get; set; } = new();
    public ProblemSettings Problems { get; set; } = new();

    public static Configuration Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= LoadConfiguration();
                }
            }
            return _instance;
        }
    }

    private static Configuration LoadConfiguration()
    {
        try
        {
            if (File.Exists(ConfigFile))
            {
                var json = File.ReadAllText(ConfigFile);
                var config = JsonSerializer.Deserialize<Configuration>(json);
                if (config != null)
                {
                    Logger.Info("Configuration loaded from file");
                    return config;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Warning($"Failed to load configuration: {ex.Message}");
        }

        // Return default configuration
        var defaultConfig = new Configuration();
        defaultConfig.SaveConfiguration();
        Logger.Info("Using default configuration");
        return defaultConfig;
    }

    public void SaveConfiguration()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(ConfigFile, json);
            Logger.Info("Configuration saved to file");
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to save configuration", ex);
        }
    }
}

public class BenchmarkSettings
{
    public int WarmupRuns { get; set; } = 10;
    public int MinBenchmarkRuns { get; set; } = 30;
    public int MaxBenchmarkRuns { get; set; } = 1000;
    public double ConfidenceLevel { get; set; } = 0.95;
    public double MarginOfError { get; set; } = 0.05;
    public bool EnableParallelExecution { get; set; } = true;
    public int MaxParallelThreads { get; set; } = Environment.ProcessorCount;
}

public class LoggingSettings
{
    public LogLevel MinimumLevel { get; set; } = LogLevel.Info;
    public bool EnableFileLogging { get; set; } = true;
    public bool EnableConsoleLogging { get; set; } = true;
    public string LogFilePath { get; set; } = "euler_solver.log";
    public int MaxLogFileSizeMB { get; set; } = 10;
    public int MaxLogFiles { get; set; } = 5;
}

public class ProblemSettings
{
    public int MaxProblemId { get; set; } = 900;
    public bool EnableCaching { get; set; } = true;
    public int CacheSizeMB { get; set; } = 100;
    public bool EnableProgressReporting { get; set; } = true;
    public int ProgressUpdateIntervalMs { get; set; } = 100;
}
