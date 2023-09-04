using NodaTime;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Noda.Data;

public class XX
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Culture { get; set; } = "en-US";
    public string TimeZone { get; set; } = "Asia/Damascus";
    public string Calendar { get; set; } = "Gregorian";

    public static IEnumerable<string> GetCultures() =>
        CultureInfo.GetCultures(CultureTypes.AllCultures).Select(c => c.Name).OrderBy(x => x);
    public static IEnumerable<string> GetTimeZones() =>
        //DateTimeZoneProviders.Tzdb.Ids.OrderBy(x => x);
        TimeZoneInfo.GetSystemTimeZones().Select(x => x.Id).OrderBy(x => x);
    public static IEnumerable<string> GetCalendars(string culture) =>
        //CalendarSystem.Ids.OrderBy(x => x);
        (new CultureInfo(culture)).OptionalCalendars.Select(x => GetNormalizedName(x.GetType().Name))
        .OrderBy(x => x);

    public static string GetNormalizedName(string name)
    {
        Regex r = new Regex(
    @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])");

        return r.Replace(name, " ");
    }
}
