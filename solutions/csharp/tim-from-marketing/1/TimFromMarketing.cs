using System.Text;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        var idPrint = id.HasValue ? $"[{id}] - " : string.Empty;
        var departmentPrint = department?.ToUpperInvariant() ?? "OWNER";
        return $"{idPrint}{name} - {departmentPrint}";
    }
}
