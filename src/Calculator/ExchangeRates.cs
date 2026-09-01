namespace Calculator;

/// <summary>
/// Exchange rates by currency pair and the date from which each rate applied.
/// A rate stays in force until the next one for the same pair starts.
/// </summary>
public static class ExchangeRates
{
    private sealed record Rate(string From, string To, DateOnly ValidFrom, decimal Value);

    private static readonly Rate[] Table =
    [
        new("EUR", "USD", new DateOnly(2026, 1, 1), 1.0800m),
        new("EUR", "USD", new DateOnly(2026, 7, 1), 1.1150m),
        new("USD", "EUR", new DateOnly(2026, 1, 1), 0.9259m),
        new("USD", "EUR", new DateOnly(2026, 7, 1), 0.8969m),
        new("EUR", "GBP", new DateOnly(2026, 1, 1), 0.8500m),
        new("EUR", "GBP", new DateOnly(2026, 7, 1), 0.8620m),
        new("GBP", "EUR", new DateOnly(2026, 1, 1), 1.1765m),
        new("GBP", "EUR", new DateOnly(2026, 7, 1), 1.1601m),
        new("USD", "GBP", new DateOnly(2026, 1, 1), 0.7870m),
        new("GBP", "USD", new DateOnly(2026, 1, 1), 1.2706m),
    ];

    /// <summary>The rate in force for the pair on the given date. Throws if the pair is unknown or the date is before the first rate.</summary>
    public static decimal RateOn(string from, string to, DateOnly date)
    {
        if (from == to) return 1m;

        Rate? best = null;
        foreach (var r in Table)
        {
            if (r.From != from || r.To != to || r.ValidFrom > date) continue;
            if (best is null || r.ValidFrom > best.ValidFrom) best = r;
        }

        return best?.Value ?? throw new InvalidOperationException($"No exchange rate for {from}/{to} on {date:yyyy-MM-dd}.");
    }
}
