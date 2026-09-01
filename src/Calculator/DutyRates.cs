namespace Calculator;

/// <summary>
/// Ad valorem duty rates by the first four digits of the commodity code.
/// These are the live values; the wiki points here rather than copying them.
/// </summary>
public static class DutyRates
{
    private static readonly IReadOnlyDictionary<string, decimal> Rates = new Dictionary<string, decimal>
    {
        ["0803"] = 0.075m, // bananas and plantains
        ["0804"] = 0.058m, // dates, figs, pineapples, avocados, mangoes
        ["0805"] = 0.064m, // citrus
        ["0810"] = 0.090m, // berries and other fresh fruit
    };

    public const decimal DefaultRate = 0.12m;

    public static decimal RateFor(string commodityCode)
    {
        ArgumentNullException.ThrowIfNull(commodityCode);
        var heading = commodityCode.Length >= 4 ? commodityCode[..4] : commodityCode;
        return Rates.TryGetValue(heading, out var rate) ? rate : DefaultRate;
    }
}
