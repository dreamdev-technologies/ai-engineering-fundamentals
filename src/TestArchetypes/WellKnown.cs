namespace TestArchetypes;

/// <summary>
/// Names for the values the business talks about, so tests, stories and wiki pages use the same words
/// and a change to a value is made in one place.
/// </summary>
public static class WellKnown
{
    public static class Currencies
    {
        public const string Euro = "EUR";
        public const string Dollar = "USD";
        public const string Sterling = "GBP";
    }

    public static class Origins
    {
        public const string CostaRica = "CR";
        public const string Ecuador = "EC";
        public const string SouthAfrica = "ZA";
        public const string Spain = "ES";
    }

    /// <summary>Growers with negotiated terms, as named in the wiki gotchas. The legacy service matches on these strings.</summary>
    public static class Growers
    {
        public const string FincaVerde = "Finca Verde";
        public const string Sunrise = "Sunrise";
        public const string AnyOtherGrower = "Rio Claro";
    }

    public static class Dates
    {
        /// <summary>The last day of the first-half price list. The legacy service prices this day off the next list.</summary>
        public static readonly DateOnly LastDayOfTheFirstHalfPriceList = new(2026, 6, 30);

        /// <summary>The day the exchange rates change; see ExchangeRates in src/Calculator.</summary>
        public static readonly DateOnly TheRateChange = new(2026, 7, 1);

        public static readonly DateOnly SixDaysBeforeTheRateChange = new(2026, 6, 25);

        public static readonly DateOnly TwoDaysAfterTheRateChange = new(2026, 7, 3);

        /// <summary>Inside the winter citrus season, when the 0805 duty rate is higher.</summary>
        public static readonly DateOnly InTheCitrusSeason = new(2026, 1, 20);
    }
}
