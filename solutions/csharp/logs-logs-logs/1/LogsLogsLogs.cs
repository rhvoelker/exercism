using System.Text.RegularExpressions;

public enum LogLevel {
    Unknown = 0,
    Trace = 1,
    Debug = 2,
    Info = 4,
    Warning = 5,
    Error = 6,
    Fatal = 42
}

static class LogLine
{
    private static Dictionary<string, LogLevel> _logLevels = new() {
        { "TRC", LogLevel.Trace },
        { "DBG", LogLevel.Debug },
        { "INF", LogLevel.Info },
        { "WRN", LogLevel.Warning },
        { "ERR", LogLevel.Error },
        { "FTL", LogLevel.Fatal }
    };

    private static Regex _logLineRegex = new(@"^\[(?<LogLevel>[^\]]+)\]");
    
    public static LogLevel ParseLogLevel(string logLine)
    {
        var match = _logLineRegex.Match(logLine);

        if (!match.Success) {
            return LogLevel.Unknown;
        }

        return _logLevels.GetValueOrDefault(match.Groups["LogLevel"].Value, LogLevel.Unknown);
    }

    public static string OutputForShortLog(LogLevel logLevel, string message) => $"{(int)logLevel}:{message}";
}
