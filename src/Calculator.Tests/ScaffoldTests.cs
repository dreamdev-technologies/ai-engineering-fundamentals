using Xunit;
using TestArchetypes;
using static TestArchetypes.ConsignmentBuilder;
using static TestArchetypes.LineItemBuilder;

namespace Calculator.Tests;

/// <summary>
/// Proves the wiring works. Activity 05 adds real acceptance tests alongside this file;
/// use the builders the way this test does.
/// </summary>
public class ScaffoldTests
{
    [Fact]
    public void Builders_produce_a_realistic_default_consignment()
    {
        Consignment consignment = AConsignment()
            .WithLines(ALineItem().WithQuantity(40), ALineItem().WithCommodityCode("08051020").WithWeightKg(600m));

        Assert.Equal(2, consignment.Lines.Count);
        Assert.Equal("EUR", consignment.Freight.Currency);
        Assert.Equal(40, consignment.Lines[0].Quantity);
    }

    [Fact]
    public void Duty_rates_and_exchange_rates_are_readable()
    {
        Assert.Equal(0.075m, DutyRates.RateFor("08039010"));
        Assert.Equal(1.08m, ExchangeRates.RateOn("EUR", "USD", new DateOnly(2026, 3, 1)));
    }
}
