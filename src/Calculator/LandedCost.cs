namespace Calculator;

/// <summary>What one lot actually cost to get to the door.</summary>
public sealed record LandedCostLine(
    LineItem Item,
    Money ProductCost,
    Money FreightShare,
    Money Duty,
    Money Total);

/// <summary>The landed cost of a whole consignment, line by line.</summary>
public sealed record LandedCost(
    string Reference,
    IReadOnlyList<LandedCostLine> Lines,
    Money Total);

/// <summary>A price to a customer, in the customer currency, with margin applied.</summary>
public sealed record CustomerQuote(
    string Reference,
    string Currency,
    IReadOnlyList<CustomerQuoteLine> Lines,
    Money Total);

public sealed record CustomerQuoteLine(
    LineItem Item,
    Money LandedCost,
    Money Price);
