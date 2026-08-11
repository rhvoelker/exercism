using System.Globalization;

public enum Location
{
    NewYork,
    London,
    Paris
}

public enum AlertLevel
{
    Early,
    Standard,
    Late
}

public static class Appointment
{
    public static DateTime ShowLocalTime(DateTime dtUtc) => TimeZoneInfo.ConvertTimeFromUtc(dtUtc, TimeZoneInfo.Local);

    public static DateTime Schedule(string appointmentDateDescription, Location location) => 
        TimeZoneInfo.ConvertTimeToUtc(
            DateTime.Parse(appointmentDateDescription),
            LocationToTimeZone(location));

    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel) => alertLevel switch
    {
        AlertLevel.Early => appointment.AddDays(-1),
        AlertLevel.Standard => appointment.AddMinutes(-105),
        AlertLevel.Late => appointment.AddMinutes(-30),
        _ => appointment
    };

    public static bool HasDaylightSavingChanged(DateTime dt, Location location)
    {
        var timeZone = LocationToTimeZone(location);
        return timeZone.IsDaylightSavingTime(dt.AddDays(-7)) != timeZone.IsDaylightSavingTime(dt);
    }

    public static DateTime NormalizeDateTime(string dtStr, Location location) =>
        DateTime.TryParse(dtStr, LocationToCulture(location), out var dt)
            ? dt
            : DateTime.MinValue;

    private static TimeZoneInfo LocationToTimeZone(Location location) => location switch
    {
        Location.NewYork => TimeZoneInfo.FindSystemTimeZoneById("America/New_York"),
        Location.London => TimeZoneInfo.FindSystemTimeZoneById("Europe/London"),
        Location.Paris => TimeZoneInfo.FindSystemTimeZoneById("Europe/Paris"),
        _ => TimeZoneInfo.Utc
    };

    private static CultureInfo LocationToCulture(Location location) => location switch
    {
        Location.NewYork => new CultureInfo("en-US"),
        Location.London => new CultureInfo("en-GB"),
        Location.Paris => new CultureInfo("fr-FR"),
        _ => CultureInfo.InvariantCulture
    };
}
