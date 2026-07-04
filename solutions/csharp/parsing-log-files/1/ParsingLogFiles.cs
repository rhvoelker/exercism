using System.Linq;
using System.Text.RegularExpressions;

public class LogParser
{
    private static readonly Regex LineRegex = new(@"^\[(?<Level>TRC|DBG|INF|WRN|ERR|FTL)\][^\[\]]+$");
    private static readonly Regex SeparatorRegex = new(@"<[^<>]+>");
    private static readonly Regex PasswordRegex = new(@"""[^""]*password[^""]*""", RegexOptions.IgnoreCase);
    private static readonly Regex EndOfLineRegex = new(@"end-of-line\d+");
    private static readonly Regex WeakPasswordRegex = new(@"password[^\s]+", RegexOptions.IgnoreCase);
    
    public bool IsValidLine(string text) => LineRegex.IsMatch(text);

    public string[] SplitLogLine(string text) => SeparatorRegex.Split(text);

    public int CountQuotedPasswords(string lines) => PasswordRegex.Count(lines);

    public string RemoveEndOfLineText(string line) => EndOfLineRegex.Replace(line, string.Empty);

    public string[] ListLinesWithPasswords(string[] lines) => lines
        .Select(line =>
        {
            var match = WeakPasswordRegex.Match(line);
            return match.Success ? $"{match.Value}: {line}" : $"--------: {line}";
        })
        .ToArray();
}
