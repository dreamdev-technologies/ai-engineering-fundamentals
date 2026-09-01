namespace Calculator;

/// <summary>
/// Turns a consignment into a landed cost and a customer quote.
/// Deliberately empty: activity 05 fills it in, tests first.
/// </summary>
public sealed class LandedCostCalculator
{
    public LandedCost Calculate(Consignment consignment)
    {
        throw new NotImplementedException("Activity 05, stories 1 and 2: implement against the acceptance tests.");
    }

    public CustomerQuote Quote(Consignment consignment, string customerCurrency, DateOnly quoteDate)
    {
        throw new NotImplementedException("Activity 05, story 3: implement against the acceptance tests.");
    }
}
