using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Noda.Data;
using NodaMoney;
using NodaMoney.Extensions;
using NodaTime;
using System.Globalization;

namespace Noda.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        TestMoney();
         //TestTime();
        TestCalendar();
        // TestFormatProviders();

        ;
    }

    private void TestCalendar()
    {
        var company = new XX();
        var cultures = XX.GetCultures();
        var timezones = XX.GetTimeZones();
        var calendars = XX.GetCalendars(company.Culture);
        ;
    }

    private void TestTime()
    {
        Instant now = SystemClock.Instance.GetCurrentInstant();
        ZonedDateTime nowInIsoUtc = now.InUtc();
        ZonedDateTime nowInDamascus = now.InZone(DateTimeZoneProviders.Tzdb["Asia/Damascus"]);
        ZonedDateTime nowHijri = now.InZone(DateTimeZoneProviders.Tzdb["Asia/Damascus"],
            CalendarSystem.IslamicBcl);

        

        DateTime dt = DateTime.Now;

        CultureInfo culture = new CultureInfo("ar-SY");
        CultureInfo culture1 = new CultureInfo("ar-SA");
        DateTimeZone timeZone = DateTimeZoneProviders.Tzdb["Asia/Damascus"];
        DateTimeZone timeZone1 = DateTimeZoneProviders.Tzdb["Asia/Riyadh"];
        ZonedDateTime nowZonedDateTime = new ZonedDateTime(Instant.FromDateTimeUtc(DateTime.Now.ToUniversalTime()), timeZone);
        ZonedDateTime nowZonedDateTime1 = new ZonedDateTime(Instant.FromDateTimeUtc(DateTime.Now.ToUniversalTime()), timeZone1);
        String nodaFormat = nowZonedDateTime.LocalDateTime.ToString(culture.DateTimeFormat.LongDatePattern, culture1);


        var r = culture.DateTimeFormat;
        r.Calendar = culture.OptionalCalendars[1];
        ;

        DateTime dtUtc = dt.ToUniversalTime();
        LocalDateTime ldt = LocalDateTime.FromDateTime(dtUtc);

        var newLdt = ldt.WithCalendar(CalendarSystem.IslamicBcl);
        var ff = newLdt.ToString("D", culture1);

        var london = DateTimeZoneProviders.Tzdb["Asia/Damascus"];
        var before = london.AtStrictly(ldt);
        ;
    }

    private static void TestMoney()
    {
        ;
        var syp = NodaMoney.Currency.FromCode("SYP");
        var pts = new NodaMoney.CurrencyBuilder("PTS", "virtual")
        {
            DecimalDigits = 0,
            EnglishName = "Points",
            Symbol = "pts",
            ISONumber = "PTS"

        }.Build();
        
        var all = Currency.GetAllCurrencies().ToList();
       
        Money m = new Money(1, syp);
        Money m1 = new Money(1, pts);
        ExchangeRate r = new ExchangeRate(syp, pts, 2,);
        var y = m + r.Convert(m1);

        var m2 = r.Convert(m);
        var m3 = r.Convert(m1);
        CultureInfo en = new CultureInfo("en-US");
        CultureInfo sy = new CultureInfo("ar-SY");
        Money k = new Money(560.2, Currency.FromCode("OMR"));
        var v = k.ToString($"c{k.Currency.DecimalDigits}");
        m = new Money(300, syp);
        var s = m.ToString("I", sy.NumberFormat);
    }
}
