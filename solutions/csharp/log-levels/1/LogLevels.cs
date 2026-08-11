using System.Text.RegularExpressions;

static class LogLine
{
    private static readonly Regex LineRegex = new(@"^\[(?<Level>[^\]]+)\]:(?<Message>.+)$");
    
    public static string Message(string logLine) => FormatLineMatch(logLine, (message, _) => message.Trim());

    public static string LogLevel(string logLine) => FormatLineMatch(logLine, (_, level) => level.ToLowerInvariant());

    public static string Reformat(string logLine) => FormatLineMatch(
        logLine,
        (message, level) => string.Format("{0} ({1})", message.Trim(), level.ToLowerInvariant()));

    private static string FormatLineMatch(string logLine, Func<string, string, string> formatFn)
    {
        var match = LineRegex.Match(logLine);

        if (!match.Success)
        {
            return string.Empty;
        }

        return formatFn(match.Groups["Message"].Value, match.Groups["Level"].Value);
    }
}
